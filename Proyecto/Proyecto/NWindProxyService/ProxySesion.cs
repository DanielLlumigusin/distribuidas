using Entities;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWindProxyService
{
    public class ProxySesion : ISesionService
    {
        public async Task<Sesione> CreateSesionAsync(Sesione sesione)
        {
            ProxyTask task = new ProxyTask();
            return await task.SendPost<Sesione, Sesione>("/api/nwind/createsesion/", sesione);
        }
        public Sesione CreateSesion(Sesione sesione)
        {
            Sesione result = null;
            Task.Run(async() => await CreateSesionAsync(sesione)).Wait();
            return result;
        }
        public async Task<Sesione> FilterSesionAsync(int UserId)
        {
            ProxyTask task = new ProxyTask();
            return await task.SendGet<Sesione>($"/api/nwind/filtersesion/{UserId}");
        }

        public Sesione FilterSesion(int UserId)
        {
            Sesione sesione = null;
            Task.Run(async()=> await FilterSesionAsync(UserId)).Wait();
            return sesione;
        }

        public async Task<Sesione> UpdateSesionAsync(Sesione sesione)
        {
            ProxyTask task = new ProxyTask();
            return await task.SendPost<Sesione, Sesione>("/api/nwind/updatesesion", sesione);
        }
        public bool UpdateSesion(Sesione sesione)
        {
            bool result = false;
            Task.Run(async()=> await UpdateSesionAsync(sesione)).Wait();
            return result;
        }
    }
}
