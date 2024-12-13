using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public class RolSecurity
    {

        public bool IsAdmin(Usuario usuario)
        {
            return usuario.Rol == "Admin";
        }

        public bool IsViewer(Usuario usuario)
        {
            return usuario.Rol == "Viewer";
        }

        public bool IsEditor(Usuario usuario)
        {
            return usuario.Rol == "Editor";
        }

    }
}
