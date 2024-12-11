using Entities;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWindProxyService
{
    public class ProxyAuditoria : IAuditoriaService
    {
        public async Task<Auditoria> CreateAudtoriaAsync(Auditoria auditoria)
        {
            ProxyTask task = new ProxyTask();
            return await task.SendPost<Auditoria, Auditoria>("/api/Auditoria/CreateAuditoria", auditoria);
        }
        public Auditoria CreateAuditoria(Auditoria auditoria)
        {
            Auditoria result = null;
            Task.Run(async()=>await CreateAudtoriaAsync(auditoria)).Wait();
            return result;
        }

        public async Task<List<Auditoria>> GetAllAuditoriaAsync(string EventType)
        {
            ProxyTask task = new ProxyTask();
            return await task.SendGet<List<Auditoria>>($"/api/Auditoria/getallauditoria/{EventType}");
        }

        public List<Auditoria> GetAllAuditoria(string EventType)
        {
            List<Auditoria> resutl = null;
            Task.Run(async()=>await  GetAllAuditoriaAsync(EventType)).Wait();
            return resutl;
            
        }
    }
}
