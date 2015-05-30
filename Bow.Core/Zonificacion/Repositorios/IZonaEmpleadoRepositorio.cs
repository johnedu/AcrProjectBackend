using Abp.Domain.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Repositorios
{
    public interface IZonaEmpleadoRepositorio : IRepository<ZonaEmpleado>
    {
        List<ZonaEmpleado> GetAllWithPersonaAndZonaAndTipoAndEstado(int zonaId, int estadoActivoId);

        ZonaEmpleado GetWithEmpleadoAndZonaAndTipoAndEstado(int zonaId);

    }
}
