using BILL;
using Entities;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Service.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpGet]
        public Usuario FilterUsuario(string username)
        {
            var usuarioLogic = new UsuarioLogic();
            var result = usuarioLogic.FilterUsuario(username);
            return result;
        }

        [HttpPost]
        public Usuario CreateUsuario(Usuario usuario)
        {
            var usuarioLogic = new UsuarioLogic();
            var result = usuarioLogic.CreateUsuario(usuario);
            return result;
        }

        [HttpDelete]
        public bool DeleteUsuario(int id)
        {
            var usuarioLogic = new UsuarioLogic();
            var result = usuarioLogic.DeleteUsuario(id);
            return result;
        }

        [HttpPost]
        public bool EditUsuario(Usuario usuario)
        {
            var usuarioLogic = new UsuarioLogic();
            var result = usuarioLogic.EditUsuario(usuario);
            return result;
        }


        [HttpGet]
        public List<Usuario> GetUsuarios(string name)
        {
            var usuarioLogic = new UsuarioLogic();
            var result = usuarioLogic.GetUsuarios(name);
            return result;
        }
    }
}