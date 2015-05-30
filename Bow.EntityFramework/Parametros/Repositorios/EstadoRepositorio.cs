using Bow.EntityFramework.Repositories;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Parametros.Repositorios
{
    public class EstadoRepositorio : BowRepositoryBase<Estado>, IEstadoRepositorio
    {
        public EstadoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }


        public List<Estado> GetWithNombreEstado(int Id)
        {
            var query = GetAll().Where(l => l.ParametroId == Id).Include(l => l.EstadoNombreEstado).ToList();
            return query;
        }

        public Estado GetByNombreEstadoAndNombreParametro(string nombreEstado, string nombreParametro)
        {
            return GetAll().Where(e => e.EstadoNombreEstado.Nombre == nombreEstado && e.ParametroEstado.Nombre == nombreParametro).Include(e => e.ParametroEstado).FirstOrDefault();
        }
    }
}
    