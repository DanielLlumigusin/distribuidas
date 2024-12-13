using Entities;
using System;
using System.Net.Sockets;
using System.Net;

namespace Security
{
    public class RegisterSecurity
    {
        public bool AddRegister(string username)
        {
            bool result = false;
            var Proxy = new BILL.UsuarioLogic();
            var ProxySesion = new BILL.SesionLogic();
            var ProxyAuditoria = new BILL.AuditoriaLogic();
            var ProxyToken = new BILL.TokenLogic();
            var SecurityToken = new Security.TokenSecurity();

            // Verifica que el usuario exista antes de usarlo
            var userFind = Proxy.FilterUsuario(username);
            if (userFind == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            // Inicialización de objetos para evitar NullReferenceException
            TokensSeguridad tokensSeguridad = new TokensSeguridad();
            tokensSeguridad.UserId = userFind.Id;
            tokensSeguridad.Token = SecurityToken.GeneratedEncryptToken($"{userFind}", "SuperClaveMuySeguraYLarga1234567");
            tokensSeguridad.TokenType = "CSRF";
            tokensSeguridad.ExpiresAt = DateTime.Now.AddDays(1);

            // Creación del token
            ProxyToken.CreateToken(tokensSeguridad);

            // Inicialización de la sesión
            Sesione newSesion = new Sesione();
            newSesion.UserId = userFind.Id;
            newSesion.SessionToken = tokensSeguridad.Token;
            newSesion.CreatedAt = DateTime.Now;
            newSesion.ExpiresAt = DateTime.Now.AddDays(1);

            // Creación de la sesión
            ProxySesion.CreateSesion(newSesion);

            // Inicialización de la auditoría
            Auditoria newAuditoria = new Auditoria();
            newAuditoria.UserId = userFind.Id;
            newAuditoria.EventType = "LoginSuccess";
            newAuditoria.EventDescription = "ok";
            newAuditoria.IpAddress = GetLocalIPAddress();
            newAuditoria.EventTime = DateTime.Now;
            // Creación de la auditoría
            ProxyAuditoria.CreateAuditoria(newAuditoria);

            result = true;  // Asignamos true para indicar que todo fue exitoso
            return result;
        }

        static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No se encontró una dirección IPv4 en esta máquina.");
        }
    }
}
