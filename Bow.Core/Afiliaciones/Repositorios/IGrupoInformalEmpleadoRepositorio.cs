using Abp.Domain.Repositories;
using Bow.Afiliaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Repositorios
{
    public interface IGrupoInformalEmpleadoRepositorio : IRepository<GrupoInformalEmpleado>
    {
        /// <summary>
        ///     Obtiene la lista de empleados de un grupo informal
        /// </summary>
        /// <param name="GrupoInformalId">Id del grupo informal</param>
        /// <returns>Retorna una lista de objetos de <see cref="Bow.Afiliaciones.Entidades.GrupoInformalEmpleado"/> con la información de cada empleado</returns>
        List<GrupoInformalEmpleado> GetAllEmpleadosByGrupoInformal(int GrupoInformalId);
    }
}
