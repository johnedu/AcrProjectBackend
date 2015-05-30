using Abp.Domain.Repositories;
using Bow.Afiliaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Repositorios
{
    public interface IPlanExequialRepositorio : IRepository<PlanExequial>
    {
        /// <summary>
        ///     Obtiene el plan exequial a partir del id con la información de la moneda
        /// </summary>
        /// <param name="Id">Id del plan exequial</param>
        /// <returns>Retorna un objeto de <see cref="Bow.Application.Afiliaciones.Entidades.PlanExequial"/> con la información del plan exequial</returns>
        PlanExequial GetWithMoneda(int Id);

        List<PlanExequial> GetAllPlanesExequialesEmpresarialesBySucursalAndEmpresaAndRecuado(int SucursalId, int? EmpresaId, int? RecaudoId);

        List<PlanExequial> GetAllPlanesExequialesGruposBySucursalAndGrupoAndRecuado(int SucursalId, int? GrupoId, int? RecaudoId);

        List<PlanExequial> GetAllPlanesExequialesFamiliaresBySucursalAndRecuado(int SucursalId, int? RecaudoId);
    }
}
