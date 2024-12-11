using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLC
{
    public interface IAuditoriaService
    {
        Auditoria CreateAuditoria(Auditoria auditoria);

        List<Auditoria> GetAllAuditoria();
    }
}
