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
    public class SufijoLocalidadRepositorio : BowRepositoryBase<SufijoLocalidad>, ISufijoLocalidadRepositorio
    {

        public SufijoLocalidadRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }
        public List<SufijoLocalidad> GetAllListWithSufijoByLocalidad(int localidadId)
        {
            return GetAll().Where(su => su.LocalidadId == localidadId).Include(su => su.SufijoSufijoLocalidad).ToList();
        }

        public SufijoLocalidad GetWithSufijo(int id)
        {
            return GetAll().Where(su => su.Id == id).Include(su => su.SufijoSufijoLocalidad).FirstOrDefault();
        }
    }
}
