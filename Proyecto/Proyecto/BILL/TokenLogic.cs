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
    public class TokenLogic : ITokenService
    {
        public TokensSeguridad CreateToken(TokensSeguridad token)
        {
            TokensSeguridad result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Create<TokensSeguridad>(token);
            }

            return result;
        }

        public TokensSeguridad FilterToken(int usuarioId)
        {
            TokensSeguridad result = null;
            using (var r = RepositoryFactory.CreateRepository()) { 
                result =  r.Retrieve<TokensSeguridad>(t => t.UserId == usuarioId);
            }
            return result;
        }
    }
}
