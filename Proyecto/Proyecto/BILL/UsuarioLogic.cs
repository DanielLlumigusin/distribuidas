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
            throw new NotImplementedException();
        }

        public bool EditUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario FilterUsuario(string name)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> GetUsuarios()
        {
            throw new NotImplementedException();
        }
    }
}
