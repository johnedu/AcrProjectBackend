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
    public class GrupoParentescoRangoRepositorio : BowRepositoryBase<GrupoParentescoRango>, IGrupoParentescoRangoRepositorio
    {
        public GrupoParentescoRangoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<GrupoParentescoRango> GetWithPlanExequial(int parentescoId, int parentescoEdad)
        {
            return GetAll().Where(g => g.GrupoFamiliarParentesco.ParentescoId == parentescoId && g.EdadMinima <= parentescoEdad && g.EdadMaxima >= parentescoEdad)
                .Include(p => p.GrupoFamiliarParentesco.Parentesco)
                .Include(r => r.GrupoFamiliarParentesco.GrupoFamiliar.PlanExequialGrupoFamiliar.PlanExequialSucursales).ToList();
        }

        public List<GrupoParentescoRango> GetWithParentescoByGrupo(int grupoFamiliarId)
        {
            return GetAll().Where(g => g.GrupoFamiliarParentesco.GrupoFamiliarId == grupoFamiliarId)
                .Include(p => p.GrupoFamiliarParentesco.Parentesco)
                .OrderBy(p => p.GrupoFamiliarParentesco.Parentesco.Posicion)
                .ThenBy(p => p.EdadMinima)
                .ToList();
        }

        public List<GrupoParentescoRango> GetWithParentescoByGrupoAndParentesco(int parentescoId, int grupoFamiliarId)
        {
            return GetAll().Where(g => g.GrupoFamiliarParentesco.GrupoFamiliarId == grupoFamiliarId && g.GrupoFamiliarParentesco.ParentescoId == parentescoId)
                .Include(p => p.GrupoFamiliarParentesco.Parentesco)
                .OrderBy(p => p.GrupoFamiliarParentesco.Parentesco.Posicion)
                .ThenBy(p => p.EdadMinima)
                .ToList();
        }
    }
}
