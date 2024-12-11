using DAL;
using Entities;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILL
{
    public class UsuarioLogic : IUsuarioService
    {
        public Usuario CreateUsuario(Usuario usuario)
        {
            Usuario res = null;
            using(var r = RepositoryFactory.CreateRepository())
            {
                Usuario result = r.Retrieve<Usuario>(u => u.Username == usuario.Username);
                if (result == null) { 
                    res = r.Create<Usuario>(usuario);
                }
                else
                {

                }
            }
            return res;
        }

        public bool DeleteUsuario(Usuario usuario)
        {
            bool result = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                Usuario res = null;
                try
                {
                    res = r.Retrieve<Usuario>(u => u.Rol == "Admin");
                    if (res == null)
                    {
                        result = r.Delete<Usuario>(usuario);
                    }
                }
                catch
                {

                }
            }
            return result;
        }

        public bool EditUsuario(Usuario usuario)
        {
            bool result = false;
            using( var r = RepositoryFactory.CreateRepository())
            {
                result = r.Delete<Usuario>(usuario);
            }

            return result;
        }

        public Usuario FilterUsuario(string username)
        {
            Usuario result = null;
            using (var r = RepositoryFactory.CreateRepository()) { 
                result = r.Retrieve<Usuario>(u => u.Username == username);
            }
            return result;
        }

        public List<Usuario> GetUsuarios(string name)
        {
            List<Usuario> result = null;
            using (var r = RepositoryFactory.CreateRepository()) {
                result = r.Filter<Usuario>(u => u.Name == name);
            }
            return result;
        }
    }
}
