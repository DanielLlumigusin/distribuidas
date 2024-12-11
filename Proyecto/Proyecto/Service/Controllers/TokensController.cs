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
    public class TokensController : ApiController, ITokenService
    {

        [HttpPost]
        public TokensSeguridad CreateToken(TokensSeguridad token)
        {
            var tokenLogic = new TokenLogic();
            var result = tokenLogic.CreateToken(token);
            return result;
        }

        [HttpGet]
        public TokensSeguridad FilterToken(int usuarioId)
        {
            var tokenLogic = new TokenLogic();
            var result = tokenLogic.FilterToken(usuarioId);
            return result;
        }
    }
}