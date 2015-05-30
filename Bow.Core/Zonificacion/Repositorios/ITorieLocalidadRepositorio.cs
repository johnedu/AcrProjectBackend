using Abp.Domain.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Repositorios
{
    public interface ITorieLocalidadRepositorio : IRepository<TorieLocalidad>
    {
        /// <summary>
        /// Obtiene una lista con los tipos de orientación que están asignados en la localidad indicada
        /// </summary>
        /// <param name="localidadid">Código de la localidad a consultar los tipos de orientación registrados</param>
        /// <returns></returns>
        List<TorieLocalidad> GetAllListWithTipoOrientacionByLocalidad(int localidadId);

        /// <summary>
        /// Retorna el TorieLocalidad identificado con el código indicado, y hace la carga de los datos de TipoOrientacion
        /// </summary>
        /// <param name="id">Código del TorieLocalidad a consultar</param>
        /// <returns></returns>
        TorieLocalidad GetWithTipoOrientacion(int id);
    }
}
