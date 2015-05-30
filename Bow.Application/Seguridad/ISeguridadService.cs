using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Seguridad
{
    public interface ISeguridadService : IApplicationService
    {
        Task CrearTenant();
    }
}
