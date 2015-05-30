using Abp.EntityFramework;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bow.Zonificacion.Repositorios
{
    public class ZonaEmpleadoRepositorio : BowRepositoryBase<ZonaEmpleado>, IZonaEmpleadoRepositorio
    {
        public ZonaEmpleadoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<ZonaEmpleado> GetAllWithPersonaAndZonaAndTipoAndEstado(int zonaId, int estadoActivoId)
        {
            return GetAll().Where(ze => ze.ZonaId == zonaId && ze.EstadoId == estadoActivoId).Include(ze => ze.EmpleadoZonaEmpleado.PersonaEmpleado)
                .Include(ze => ze.ZonaZonaEmpleado)
                .Include(ze => ze.TipoZonaEmpleado)
                .Include(ze => ze.EstadoZonaEmpleado).ToList();
        }

        public ZonaEmpleado GetWithEmpleadoAndZonaAndTipoAndEstado(int zonaempleadoId)
        {
            return GetAll().Where(ze => ze.Id == zonaempleadoId).Include(ze => ze.EmpleadoZonaEmpleado.PersonaEmpleado)
                .Include(ze => ze.ZonaZonaEmpleado)
                .Include(ze => ze.TipoZonaEmpleado)
                .Include(ze => ze.EstadoZonaEmpleado).FirstOrDefault();
        }

    }
}
