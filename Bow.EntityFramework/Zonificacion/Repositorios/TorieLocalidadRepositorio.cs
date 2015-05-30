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
    public class TorieLocalidadRepositorio : BowRepositoryBase<TorieLocalidad>, ITorieLocalidadRepositorio
    {
        public TorieLocalidadRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<TorieLocalidad> GetAllListWithTipoOrientacionByLocalidad(int localidadId)
        {
            return GetAll().Where(to => to.LocalidadId == localidadId).Include(to => to.TipoOrientacionTorieLocalidad).ToList();
        }

        public TorieLocalidad GetWithTipoOrientacion(int id)
        {
            return GetAll().Where(to => to.Id == id).Include(to => to.TipoOrientacionTorieLocalidad).FirstOrDefault();
        }
    }
}
