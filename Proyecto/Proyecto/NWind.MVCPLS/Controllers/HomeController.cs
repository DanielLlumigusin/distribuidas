using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Web.Mvc;
using Entities;

namespace NWind.MVCPLS.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Home()
        {
            var username = Session["Username"] as string;
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index");
            }

            var Proxy = new BILL.UsuarioLogic();

            var usuario = Proxy.FilterUsuario(username);

            if (usuario == null)
            {
                return RedirectToAction("Index");
            }

            switch (usuario.Rol)
            {
                case "Admin":
                    return RedirectToAction("AdminView");
                case "Editor":
                    return RedirectToAction("EditorView", new { username = usuario.Username });
                case "Viewer":
                    return RedirectToAction("ViewerView", new { username = usuario.Username });
                default:
                    ViewBag.Message = "Rol no reconocido.";
                    return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            var securityUsuario = new Security.UsuarioSecurity();
            var securityRegister = new Security.RegisterSecurity();

            if (securityUsuario.IsUsuarioAuthorize(username, password))
            {
                if (securityRegister.AddRegister(username))
                {
                    Session["Username"] = username;
                    return RedirectToAction("Home");
                }
                else
                {
                    ViewBag.Message = "Error al registrar la sesión.";
                }
            }
            else
            {
                ViewBag.Message = "Usuario o contraseña incorrectos.";
            }

            return View("Index");
        }

        // Vista para el rol Admin
        [HttpGet]
        public ActionResult AdminView()
        {
            var Proxy = new BILL.UsuarioLogic();
            var username = Session["Username"] as string;
            var usuarios = Proxy.FilterUsuario(username);
            var listUsuarios = Proxy.GetUsuarios(usuarios.Name);
            return View(listUsuarios);
        }

        // Vista para el rol Editor
        [HttpGet]
        public ActionResult EditorView(string username)
        {
            var Proxy = new BILL.UsuarioLogic();
            var usuario = Proxy.FilterUsuario(username);
            return View(usuario);
        }

        // Vista para el rol Viewer
        [HttpGet]
        public ActionResult ViewerView(string username)
        {
            var Proxy = new BILL.UsuarioLogic();
            var usuario = Proxy.FilterUsuario(username);
            return View(usuario);
        }

        // Métodos de CRUD para Admin

        [HttpGet]
        public ActionResult CreateUserView()
        {
            return View();
        }

        // Crear un nuevo usuario
        [HttpPost]
        public ActionResult CreateUserView(string Name, string Username,
            string PasswordHash, string Rol)
        {
            var Proxy = new Security.RegisterSecurity();
            Usuario usuario = new Usuario();
            usuario.Name = Name;
            usuario.Username = Username;
            usuario.PasswordHash = PasswordHash;
            usuario.Rol = Rol;
            
            Proxy.CreateUser(usuario);
            
            return RedirectToAction("Home");
        }

        [HttpGet]
        public ActionResult UpdateView(string Username)
        {
            var Proxy = new BILL.UsuarioLogic();
            var User = Proxy.FilterUsuario(Username);
            return View(User);
        }

        [HttpPost]
        public ActionResult UpdateView(
            string Id, string Name, string Username, string PasswordHash ,string Rol,string Status)
        {
            var Proxy = new Security.RegisterSecurity();            
            int newId = int.Parse(Id);
           
                Proxy.UpdateUser(newId,Name,Username, PasswordHash,Rol,Status);
                return RedirectToAction("Home");
            
        }

        // Eliminar un usuario no se va a incorporar porque esta dependiendo de varias tablas.
        // Sino que se le cambia de estado.
        [HttpDelete]
        public ActionResult DeleteUsuario()
        {
            return View();
        }


    }
}
