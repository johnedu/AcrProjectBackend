using Abp.Domain.Repositories;
using Bow.Afiliaciones.Entidades;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Repositorios
{
    public interface IBeneficioAdicionalPlanExequialRepositorio : IRepository<BeneficioAdicionalPlanExequial>
    {

        List<BeneficioAdicionalPlanExequial> GetWithBeneficio(int planExequialId, int estadoId);

        List<BeneficioAdicionalPlanExequial> GetWithTipo(int planExequialId);

        BeneficioAdicionalPlanExequial GetBeneficioWithTipo(int planExequialId);

    }
}
