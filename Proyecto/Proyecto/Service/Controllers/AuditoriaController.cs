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
    public class AuditoriaController : ApiController, IAuditoriaService
    {
        [HttpPost]
        public Auditoria CreateAuditoria(Auditoria auditoria)
        {
            var auditoriaLogic = new AuditoriaLogic();
            var result = auditoriaLogic.CreateAuditoria(auditoria);
            return result;
        }

        [HttpGet]
        public List<Auditoria> GetAllAuditoria(string EventType)
        {
            var auditoriaLogic = new AuditoriaLogic();
            var result = auditoriaLogic.GetAllAuditoria(EventType);
            return result;
        }

        
    }
}