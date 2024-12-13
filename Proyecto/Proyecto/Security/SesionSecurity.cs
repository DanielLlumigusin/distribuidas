using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public class SesionSecurity
    {
        public bool ValidateSession(string token, TokensSeguridad RealToken)
        {
            var key = "ProfeNoNosPongaCeroPorfa";
            var Token = new TokenSecurity();
            var tokenforvalidate = Token.DecryptToken(token, key);
            var tokencompare = Token.DecryptToken(RealToken.Token, key);
            if (tokenforvalidate == tokencompare) { 
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
