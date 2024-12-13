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

        public bool CreateUser(Usuario usuario)
        {
            bool result = false;
            try
            {
                if (usuario == null) return false;

                var usuarioLogic = new BILL.UsuarioLogic();
                var proxyAuditoria = new BILL.AuditoriaLogic();
                var usuarioSecurity = new UsuarioSecurity();

                // Crear nuevo usuario
                Usuario newUsuario = new Usuario
                {
                    Name = usuario.Name,
                    Username = usuario.Username,
                    PasswordHash = usuarioSecurity.GenerateHashPassword(usuario.PasswordHash),
                    Rol = usuario.Rol ?? "Viewer",
                    Status = "Active", 
                    SessionAttempts = 3,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                };

                usuarioLogic.CreateUsuario(newUsuario);

                // Obtener ID del usuario creado
                var userId = usuarioLogic.FilterUsuario(newUsuario.Username);

                // Crear registro de auditoría
                Auditoria newAuditoria = new Auditoria
                {
                    UserId = userId.Id,
                    EventType = "AccountActivation",
                    EventDescription = "Usuario creado exitosamente.",
                    IpAddress = GetLocalIPAddress(),
                    EventTime = DateTime.Now
                };
                proxyAuditoria.CreateAuditoria(newAuditoria);

                result = true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: registrar logs o auditorías de error
                Console.WriteLine("Error al crear usuario: " + ex.Message);
            }
            return result;
        }

        public bool UpdateUser(int Id, string Name, string Username, string PasswordHash, string Rol, string Status)
        {
            var usuarioLogic = new BILL.UsuarioLogic();
            var proxyAuditoria = new BILL.AuditoriaLogic();
            var usuarioSecurity = new UsuarioSecurity();

            bool result = false;

            var usuario = usuarioLogic.FilterUsuario(Username);

            try
            {
                if (usuario == null) return false;

                // Crear nuevo usuario
                Usuario newUsuario = new Usuario
                {
                    Id = Id, 
                    Name = Name,
                    Username = Username,
                    PasswordHash = usuarioSecurity.GenerateHashPassword(PasswordHash),
                    Rol = Rol,
                    Status = Status,
                    SessionAttempts = usuario.SessionAttempts, 
                    CreatedAt = usuario.CreatedAt, 
                    UpdatedAt = DateTime.Now
                };

                usuarioLogic.EditUsuario(newUsuario); 

                // Obtener ID del usuario editado
                var userId = usuarioLogic.FilterUsuario(newUsuario.Username);

                // Crear registro de auditoría
                Auditoria newAuditoria = new Auditoria
                {
                    UserId = userId.Id,
                    EventType = "AccountActivation",
                    EventDescription = "Usuario editado exitosamente.",
                    IpAddress = GetLocalIPAddress(), 
                    EventTime = DateTime.Now
                };

                proxyAuditoria.CreateAuditoria(newAuditoria); 

                result = true;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error al editar usuario: " + ex.Message);
            }
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
