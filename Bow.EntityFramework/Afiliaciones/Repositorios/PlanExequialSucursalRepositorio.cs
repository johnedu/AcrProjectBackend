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

namespace Bow.Afiliaciones.Repositorios
{
    public class PlanExequialSucursalRepositorio : BowRepositoryBase<PlanExequialSucursal>, IPlanExequialSucursalRepositorio
    {
        public PlanExequialSucursalRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<PlanExequialSucursal> GetWithPlanExequial(int sucursalId)
        {
            return GetAll().Where(p => p.SucursalId == sucursalId)
                .Include(pe => pe.PlanExequialPlanExequialSucursal.GrupoFamiliarPlanExequial)
                .ToList();
        }
    }
}
