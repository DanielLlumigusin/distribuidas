using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLC
{
    public interface ITokenService
    {
        TokensSeguridad CreateToken(TokensSeguridad token);

        TokensSeguridad FilterToken(int usuarioId);

    }
}
