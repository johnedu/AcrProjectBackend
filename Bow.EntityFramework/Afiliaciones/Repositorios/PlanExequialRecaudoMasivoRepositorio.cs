using Bow.EntityFramework.Repositories;
using Bow.Afiliaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;
using Bow.Parametros.Entidades;
using System.Data.Entity;
using Bow.Empresas.Entidades;

namespace Bow.Afiliaciones.Repositorios
{
    public class PlanExequialRecaudoMasivoRepositorio : BowRepositoryBase<PlanExequialRecaudoMasivo>, IPlanExequialRecaudoMasivoRepositorio
    {
        public PlanExequialRecaudoMasivoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<RecaudoMasivo> GetAllRecaudoMasivoByPlanExequial(int planExequialId)
        {
            return GetAll().Where(b => b.PlanExequialId == planExequialId).Select(to => to.RecaudoMasivoPlanExequialRecaudoMasivo).ToList();
        }
    }
}
