using Entities;
using Newtonsoft.Json;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NWindProxyService
{
    public class ProxyUsuario : IUsuarioService
    {

        public async Task<Usuario> CreateUsuarioAsync(Usuario newUsuario)
        {
            ProxyTask task = new ProxyTask();
            return await task.SendPost<Usuario, Usuario>("/api/Usuario/createusuario",newUsuario);
        }
        public Usuario CreateUsuario(Usuario usuario)
        {
            Usuario result = null;
            Task.Run(async () => await CreateUsuarioAsync(usuario)).Wait();
            return result;
        }

        public async Task<bool> DeleteUsuarioAsync(Usuario usuario)
        {
            ProxyTask task = new ProxyTask();
            return await task.SendGet<bool>($"/api/Usuario/deleteusuario/{usuario.Id}");
        }
        public bool DeleteUsuario(Usuario usuario)
        {
            bool result = false;
            Task.Run(async () => await DeleteUsuarioAsync (usuario)).Wait();
            return result;
        }

        public async Task<bool> EditUsuarioAsync(Usuario usuario)
        {
            ProxyTask task = new ProxyTask();
            return await task.SendPost<bool,Usuario>("/api/Usuario/EditUsuario", usuario);
        }
        public bool EditUsuario(Usuario usuario)
        {
            bool result = false;
            Task.Run(async() => await EditUsuarioAsync(usuario)).Wait();
            return result;
        }

        public async Task<Usuario> FilterUsuarioAsync(string username)
        {
            ProxyTask task = new ProxyTask();
            return await task.SendGet<Usuario>($"/api/Usuario/FilterUsuario/{username}");
        }
        public Usuario FilterUsuario(string username)
        {
            Usuario result = null;
            Task.Run(async()=> await FilterUsuarioAsync(username)).Wait();
            return result;
        }

        public async Task<List<Usuario>> GetUsuariosAsync(string name)
        {
            ProxyTask task = new ProxyTask();
            return await task.SendGet<List<Usuario>>($"/api/Usuario/getusuarios/{name}");
        }
        public List<Usuario> GetUsuarios(string name)
        {
            List<Usuario> usuarios = null;
            Task.Run(async () => await GetUsuariosAsync(name)).Wait();
            return usuarios;
        }
    }
}
