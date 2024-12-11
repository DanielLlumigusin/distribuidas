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
    public class SesionLogic : ISesionService
    {
        public Sesione CreateSesion(Sesione sesione)
        {
            Sesione result = null;
            using(var r = RepositoryFactory.CreateRepository())
            {
                result = r.Create<Sesione>(sesione);
            }
            return result;
        }

        public Sesione FilterSesion(int UserId)
        {
            Sesione result = null;
            using( var r = RepositoryFactory.CreateRepository())
            {
                result = r.Retrieve<Sesione>(s=> s.UserId == UserId);
            }
            return result;
        }

        public bool UpdateSesion(Sesione sesione)
        {
            bool result = false;
            using(var r = RepositoryFactory.CreateRepository())
            {
                result = r.Update<Sesione>(sesione);
            }
            return result;
        }
    }
}
