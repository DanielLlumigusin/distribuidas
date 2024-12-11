﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLC
{
    public interface IUsuarioService
    {
        Usuario CreateUsuario(Usuario usuario);
        bool EditUsuario(Usuario usuario);
        Usuario FilterUsuario(string name);
        List<Usuario> GetUsuarios();
        bool DeleteUsuario(Usuario usuario);
    }
}