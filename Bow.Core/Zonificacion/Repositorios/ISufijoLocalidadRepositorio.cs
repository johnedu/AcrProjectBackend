using Abp.Domain.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Repositorios
{
    public interface ISufijoLocalidadRepositorio : IRepository<SufijoLocalidad>
    {
        /// <summary>
        /// Obtiene una lista con los sufijos que están asignados en la localidad indicada
        /// </summary>
        /// <param name="localidadid">Código de la localidad a consultar los sufijos registrados</param>
        /// <returns></returns>
        List<SufijoLocalidad> GetAllListWithSufijoByLocalidad(int localidadId);

        /// <summary>
        /// Retorna el SufijoLocalidad identificado con el código indicado, y hace la carga de los datos de sufijo
        /// </summary>
        /// <param name="id">Código del SufijoLocalidad a consultar</param>
        /// <returns></returns>
        SufijoLocalidad GetWithSufijo(int id);
    }
}
