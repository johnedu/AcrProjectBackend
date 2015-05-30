using Abp.EntityFramework;
using Bow.Empresas.Entidades;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Repositorios
{
    public class ActividadEconomicaRepositorio : BowRepositoryBase<ActividadEconomica>, IActividadEconomicaRepositorio
    {
        public ActividadEconomicaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }
    }
}
