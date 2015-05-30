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
    public class ZonaRepositorio : BowRepositoryBase<Zona>, IZonaRepositorio
    {
        public ZonaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<Zona> GetAllListWithZonaByLocalidad(int localidadId)
        {
            return GetAll().Where(zo => zo.LocalidadId == localidadId)
                .Include(zo => zo.TipoZona)
                .Include(zb => zb.ZonasBarrios)
                .ToList();
        }

        public Zona GetWithTipo(int id)
        {
            return GetAll().Where(to => to.Id == id).Include(to => to.TipoZona).FirstOrDefault();
        }

        public List<Zona> GetAllListWithZonaByLocalidadAndTipoZona(int localidadId, int zonaId)
        {
            return GetAll().Where(zo => zo.LocalidadId == localidadId && zo.TipoZona.Id == zonaId).Include(zo => zo.TipoZona).Include(zb => zb.ZonasBarrios).ToList();
        }
    }
}
