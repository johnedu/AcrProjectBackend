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
    public class BeneficioAdicionalPlanExequialRepositorio : BowRepositoryBase<BeneficioAdicionalPlanExequial>, IBeneficioAdicionalPlanExequialRepositorio
    {
        public BeneficioAdicionalPlanExequialRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<BeneficioAdicionalPlanExequial> GetWithBeneficio(int planExequialId, int estadoId)
        {
            return GetAll().Where(v => v.PlanExequialId == planExequialId && v.EstadoId == estadoId)
                .Include(b => b.BeneficioBeneficioAdicionalPlanExequial).ToList();
        }

        public List<BeneficioAdicionalPlanExequial> GetWithTipo(int planExequialId)
        {
            return GetAll().Where(v => v.PlanExequialId == planExequialId)
                .Include(b => b.BeneficioBeneficioAdicionalPlanExequial.TipoBeneficio).ToList();
        }

        public BeneficioAdicionalPlanExequial GetBeneficioWithTipo(int Id)
        {
            return GetAll().Where(v => v.Id == Id)
                .Include(b => b.BeneficioBeneficioAdicionalPlanExequial.TipoBeneficio).FirstOrDefault();
        }
    }
}
