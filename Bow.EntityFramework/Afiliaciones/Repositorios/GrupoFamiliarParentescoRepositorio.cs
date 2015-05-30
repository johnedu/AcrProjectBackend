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
    public class GrupoFamiliarParentescoRepositorio : BowRepositoryBase<GrupoFamiliarParentesco>, IGrupoFamiliarParentescoRepositorio
    {
        public GrupoFamiliarParentescoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<GrupoFamiliarParentesco> GetWithRangos(int PlanExequialId, int GrupoFamiliarId)
        {
            return GetAll().Where(g => g.GrupoFamiliar.PlanExequialId == PlanExequialId && g.GrupoFamiliarId == GrupoFamiliarId).OrderBy(g => g.Parentesco.Posicion).ToList();
        }

        //public List<GrupoFamiliarParentesco> GetWithParentescoAndGrupoFamiliarAndGrupoParentescoRangos(int parentescoId)
        public List<GrupoFamiliarParentesco> GetWithParentescoAndGrupoFamiliarAndGrupoParentescoRangos(int parentescoId)
        {
            return GetAll().Where(g => g.ParentescoId == parentescoId)
                .Include(p => p.Parentesco)
                .Include(r => r.GruposParentescoRango)
                .Include(gf => gf.GrupoFamiliar.PlanExequialGrupoFamiliar.PlanExequialSucursales).ToList();
        }
    }
}
