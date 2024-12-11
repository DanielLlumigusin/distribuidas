using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLC
{
    public interface ISesionService
    {
        Sesione CreateSesion(Sesione sesione);
        bool UpdateSesion(Sesione sesione);
        Sesione FilterSesion(int UserId);
    }
}
