using DAL;
using Entities;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BILL
{
    public class UsuarioLogic : IUsuarioService
    {
        public Usuario CreateUsuario(Usuario usuario)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    var existingUser = r.Retrieve<Usuario>(u => u.Username == usuario.Username);

                    if (existingUser != null)
                    {
                        throw new InvalidOperationException("El usuario ya existe.");
                    }

                    return r.Create<Usuario>(usuario);
                }
            }
            catch (Exception ex)
            {
                // Manejo o registro del error
                throw new Exception("Error al crear el usuario.", ex);
            }
        }

        public bool DeleteUsuario(int id)
        {
            bool res = false;
            var usuario = RetrieveById(id);
            if (usuario != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Delete(usuario);
                }
            }
            return res;
        }

        public bool EditUsuario(Usuario usuario)
        {
            bool res = false;
            using(var r = RepositoryFactory.CreateRepository())
            {
                Usuario temp = r.Retrieve<Usuario>
                    (u => u.Id == usuario.Id);
                if(temp == null)
                {
                    res = r.Update(usuario);
                }
            }
            return res;
        }

        public Usuario FilterUsuario(string username)
        {
            Usuario res = null;
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Retrieve<Usuario>(u => u.Username == username);
                }
            return res;
        }
        public Usuario RetrieveById(int id)
        {
            Usuario res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Retrieve<Usuario>(u => u.Id  == id);
            }
            return res;
        }

        public List<Usuario> GetUsuarios(string name)
        {
            List<Usuario> res = null;
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Filter<Usuario>(u => u.Name.Contains(name));                    
                }
            return res;
        }
    }
}
