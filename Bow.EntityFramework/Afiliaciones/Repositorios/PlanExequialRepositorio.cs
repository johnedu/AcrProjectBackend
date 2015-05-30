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
using System.Linq.Dynamic;

namespace Bow.Afiliaciones.Repositorios
{
    public class PlanExequialRepositorio : BowRepositoryBase<PlanExequial>, IPlanExequialRepositorio
    {
        public PlanExequialRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public PlanExequial GetWithMoneda(int Id)
        {
            return GetAll().Where(plan => plan.Id == Id).Include(plan => plan.MonedaPlanExequial).FirstOrDefault();
        }

        public List<PlanExequial> GetAllPlanesExequialesEmpresarialesBySucursalAndEmpresaAndRecuado(int SucursalId, int? EmpresaId, int? RecaudoId)
        {
            return GetAll()
                .Where(plan => plan.PlanEmpresarial)
                .Where(plan => plan.PlanExequialSucursales.Where(s => s.SucursalId == SucursalId).Any())
                .Where(plan => plan.PlanExequialRecaudosMasivos.Where(r => r.RecaudoMasivoId == RecaudoId).Any() || !RecaudoId.HasValue)
                .Where(plan => plan.EstadoPlanExequial.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO)
                .Include(plan => plan.MonedaPlanExequial)
                .ToList();
        }

        public List<PlanExequial> GetAllPlanesExequialesGruposBySucursalAndGrupoAndRecuado(int SucursalId, int? GrupoId, int? RecaudoId)
        {
            return GetAll()
                .Where(plan => plan.PlanParaGrupo)
                .Where(plan => plan.PlanExequialSucursales.Where(s => s.SucursalId == SucursalId).Any())
                .Where(plan => plan.PlanExequialRecaudosMasivos.Where(r => r.RecaudoMasivoId == RecaudoId).Any() || !RecaudoId.HasValue)
                .Where(plan => plan.EstadoPlanExequial.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO)
                .Include(plan => plan.MonedaPlanExequial)
                .ToList();
        }

        public List<PlanExequial> GetAllPlanesExequialesFamiliaresBySucursalAndRecuado(int SucursalId, int? RecaudoId)
        {
            return GetAll()
                .Where(plan => plan.PlanFamiliar)
                .Where(plan => plan.PlanExequialSucursales.Where(s => s.SucursalId == SucursalId).Any())
                .Where(plan => plan.PlanExequialRecaudosMasivos.Where(r => r.RecaudoMasivoId == RecaudoId).Any() || !RecaudoId.HasValue)
                .Where(plan => plan.EstadoPlanExequial.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO)
                .Include(plan => plan.MonedaPlanExequial)
                .ToList();
        }
    }
}
