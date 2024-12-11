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
    public class AuditoriaLogic : IAuditoriaService
    {
        public Auditoria CreateAuditoria(Auditoria auditoria)
        {
            Auditoria result = null;
            using( var r = RepositoryFactory.CreateRepository())
            {
                result = r.Create<Auditoria>(auditoria);
            }
            return result;
        }

        public List<Auditoria> GetAllAuditoria(string EventType)
        {
            List<Auditoria> result = null;
            using (var r = RepositoryFactory.CreateRepository()) { 
                result = r.Filter<Auditoria>(a => a.EventType == EventType);
            }
            return result;
        }
    }
}
