using Abp.Domain.Repositories;
using Bow.Afiliaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Repositorios
{
    public interface IGrupoParentescoRangoRepositorio : IRepository<GrupoParentescoRango>
    {
        List<GrupoParentescoRango> GetWithPlanExequial(int parentescoId, int parentescoEdad);

        List<GrupoParentescoRango> GetWithParentescoByGrupo(int grupoFamiliarId);

        List<GrupoParentescoRango> GetWithParentescoByGrupoAndParentesco(int grupoFamiliarId, int parentescoId);
    }
}
