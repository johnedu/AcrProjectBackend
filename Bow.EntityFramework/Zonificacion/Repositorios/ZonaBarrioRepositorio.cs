using Bow.EntityFramework.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Zonificacion.Repositorios
{
    public class ZonaBarrioRepositorio : BowRepositoryBase<ZonaBarrio>, IZonaBarrioRepositorio
    {
        public ZonaBarrioRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<ZonaBarrio> GetAllListWithBarriosByZona(int zonaId)
        {
            return GetAll().Where(zo => zo.ZonaId == zonaId).Include(zo => zo.BarrioZonaBarrio).ToList();
        }

        public List<ZonaBarrio> GetAllListWithZonaBarrioByZonaAndLocalidad(int localidadId, int tipoZonaId)
        {
            return GetAll().Where(zo => zo.ZonaZonaBarrio.LocalidadId == localidadId && zo.ZonaZonaBarrio.TipoId == tipoZonaId).ToList();
        }
    }
}
