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
    public class SesionController : ApiController, ISesionService
    {
        [HttpPost]
        public Sesione CreateSesion(Sesione sesione)
        {
            var sesionLogic = new SesionLogic();
            var result = sesionLogic.CreateSesion(sesione);
            return result;
        }
        [HttpGet]
        public Sesione FilterSesion(int UserId)
        {
            var sessionLogic = new SesionLogic();
            var result = sessionLogic.FilterSesion(UserId);
            return result;
        }
        [HttpPut]
        public bool UpdateSesion(Sesione sesione)
        {
            var sessionLogic = new SesionLogic();
            var result = sessionLogic.UpdateSesion(sesione);
            return result;
        }
    }
}