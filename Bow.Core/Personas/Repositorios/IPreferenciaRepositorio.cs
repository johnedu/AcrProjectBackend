using Abp.Domain.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Repositorios
{
    public interface IPreferenciaRepositorio : IRepository<Preferencia>
    {
        /// <summary>
        /// Trae una lista de preferencias con la cantidad de opciones de la preferencia
        /// </summary>
        /// <param name="preferenciaId"></param>
        /// <returns></returns>
        List<Preferencia> GetAllListWithOpcionPreferenciaByPreferencia();

        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Devuelve el listado de preferencias registrados, pero representa los estados con true para activo y false para inactivo
        /// </summary>
        /// <returns></returns>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        List<Preferencia> GetAllListWithNombreEstado();
    }
}
