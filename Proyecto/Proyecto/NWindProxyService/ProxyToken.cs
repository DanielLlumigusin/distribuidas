using Entities;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWindProxyService
{
    public class ProxyToken : ITokenService
    {
        public async Task<TokensSeguridad> CreateTokenAsync(TokensSeguridad token)
        {
            ProxyTask proxy = new ProxyTask();
            return await proxy.SendPost<TokensSeguridad, TokensSeguridad>("/api/Tokens/createtoken/", token);
        }
        public TokensSeguridad CreateToken(TokensSeguridad token)
        {
            TokensSeguridad tokensSeguridad = null;
            Task.Run(async() => await CreateTokenAsync(token)).Wait();
            return tokensSeguridad;
        }

        public async Task<TokensSeguridad> FilterTokenAsync(int usuarioId)
        {
            ProxyTask proxy = new ProxyTask();
            return await proxy.SendGet<TokensSeguridad>($"/api/Tokens/filtertoken/{usuarioId}");
        }
        public TokensSeguridad FilterToken(int usuarioId)
        {
            TokensSeguridad result = null;
            Task.Run(async() => await FilterTokenAsync(usuarioId)).Wait();
            return result;
        }

        
    }
}
