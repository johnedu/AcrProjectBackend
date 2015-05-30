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
    public class BeneficioPlanExequialRepositorio : BowRepositoryBase<BeneficioPlanExequial>, IBeneficioPlanExequialRepositorio
    {
        public BeneficioPlanExequialRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<BeneficioPlanExequial> GetWithBeneficio(int planExequialId, int estadoId)
        {
            return GetAll().Where(v => v.PlanExequialId == planExequialId && v.EstadoId == estadoId)
                .Include(b => b.BeneficioBeneficioPlanExequial).ToList();
        }

        public List<BeneficioPlanExequial> GetWithTipo(int planExequialId)
        {
            return GetAll().Where(v => v.PlanExequialId == planExequialId)
                .Include(b => b.BeneficioBeneficioPlanExequial.TipoBeneficio).ToList();
        }

        public BeneficioPlanExequial GetBeneficioWithTipo(int Id)
        {
            return GetAll().Where(v => v.Id == Id)
                .Include(b => b.BeneficioBeneficioPlanExequial.TipoBeneficio).FirstOrDefault();
        }


        public List<BeneficioPlanExequial> GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalOutput(int planExequialId, int tipoId, int beneficioAdicionalId)
        {
            return GetAll()
                .Where(b => b.PlanExequialId == planExequialId && b.BeneficioBeneficioPlanExequial.TipoId == tipoId && b.BeneficioId != beneficioAdicionalId).ToList();
        }
    }
}
