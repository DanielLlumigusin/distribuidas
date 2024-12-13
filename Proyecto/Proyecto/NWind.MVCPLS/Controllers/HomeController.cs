using System;
using System.Collections.Generic;
using System.Linq;
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

            // Filtra el usuario actual
            var usuario = Proxy.FilterUsuario(username);

            if (usuario == null)
            {
                return RedirectToAction("Index");
            }

            // Redirige según el rol del usuario
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
                    return View("Error"); // Opcional: Vista para manejar roles no reconocidos
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
            var usuario = Proxy.FilterUsuario(username); // Obtiene solo el usuario actual
            return View(usuario);
        }

        // Vista para el rol Viewer
        [HttpGet]
        public ActionResult ViewerView(string username)
        {
            var Proxy = new BILL.UsuarioLogic();
            var usuario = Proxy.FilterUsuario(username); // Obtiene solo el usuario actual
            return View(usuario);
        }

        // Métodos de CRUD para Admin
        [HttpPost]
        public ActionResult Create(Usuario newUsuario)
        {
            if (Session["Username"] != null && Session["Rol"] as string == "Admin")
            {
                var Proxy = new BILL.UsuarioLogic();
                Proxy.CreateUsuario(newUsuario); // Implementar lógica para crear usuario
                return RedirectToAction("AdminView");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(Usuario updatedUsuario)
        {
            var username = Session["Username"] as string;
            var rol = Session["Rol"] as string;

            var Proxy = new BILL.UsuarioLogic();
            if (rol == "Admin" || (rol == "Editor" && username == updatedUsuario.Username))
            {
                Proxy.EditUsuario(updatedUsuario);
                return rol == "Admin" ? RedirectToAction("AdminView") : RedirectToAction("EditorView", new { username });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string usernameToDelete)
        {
            if (Session["Username"] != null && Session["Rol"] as string == "Admin")
            {
                var Proxy = new BILL.UsuarioLogic();
                var user = Proxy.FilterUsuario(usernameToDelete);
                Proxy.DeleteUsuario(user.Id);
                return RedirectToAction("AdminView");
            }
            return RedirectToAction("Index");
        }
    }
}
