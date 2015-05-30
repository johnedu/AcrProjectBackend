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
    public interface IBeneficioPlanExequialRepositorio : IRepository<BeneficioPlanExequial>
    {
        List<BeneficioPlanExequial> GetWithBeneficio(int planExequialId, int estadoId);

        List<BeneficioPlanExequial> GetWithTipo(int planExequialId);

        BeneficioPlanExequial GetBeneficioWithTipo(int planExequialId);

        List<BeneficioPlanExequial> GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalOutput(int planExequialId, int tipoId, int beneficioAdicionalId);
    }
}
