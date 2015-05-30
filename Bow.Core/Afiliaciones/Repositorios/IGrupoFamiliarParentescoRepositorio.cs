using Abp.Domain.Repositories;
using Bow.Afiliaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Repositorios
{
    public interface IGrupoFamiliarParentescoRepositorio : IRepository<GrupoFamiliarParentesco>
    {
        /// <summary>
        ///     Obtiene el plan exequial a partir del id con la información de la moneda
        /// </summary>
        /// <param name="PlanExequialId">Id del plan exequial</param>
        /// <param name="GrupoFamiliarId">Id del plan exequial</param>
        /// <returns>Retorna una lista de objetos <see cref="Bow.Application.Afiliaciones.Entidades.GrupoFamiliarParentesco"/> con la información de los parentescos del grupo familiar</returns>
        List<GrupoFamiliarParentesco> GetWithRangos(int PlanExequialId, int GrupoFamiliarId);

        List<GrupoFamiliarParentesco> GetWithParentescoAndGrupoFamiliarAndGrupoParentescoRangos(int parentescoId);
    }
}
