using Bow.EntityFramework.Repositories;
using Bow.Afiliaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;
using System.Data.Entity;

namespace Bow.Afiliaciones.Repositorios
{
    public class GrupoFamiliarRepositorio : BowRepositoryBase<GrupoFamiliar>, IGrupoFamiliarRepositorio
    {
        public GrupoFamiliarRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public GrupoFamiliar GetWithPlanExequial(int grupoFamiliarId)
        {
            return GetAll().Where(g => g.Id == grupoFamiliarId)
                   .Include(g => g.PlanExequialGrupoFamiliar.PlanExequialSucursales).FirstOrDefault();
        }
    }
}
