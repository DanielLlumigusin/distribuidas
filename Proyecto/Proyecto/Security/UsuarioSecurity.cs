using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public class UsuarioSecurity
    {
        public bool IsUsuarioAuthorize(string Username, string Password)
        {
            var UserLogic = new BILL.UsuarioLogic();
            var User = UserLogic.FilterUsuario(Username);
            if (User == null || User.Status != "Active")
            {
                return false;
            }  
            return VerifyPassword(Password, User.PasswordHash);
        }

        public bool VerifyPassword(string Password, string PasswordHash) {
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(PasswordHash))
                return false;

            return BCrypt.Net.BCrypt.Verify(Password, PasswordHash);
        }

        public string GenerateHashPassword(string Password) { 
            return BCrypt.Net.BCrypt.HashPassword(Password);
        }

    }
}
