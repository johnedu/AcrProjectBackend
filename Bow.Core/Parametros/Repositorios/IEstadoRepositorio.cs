using Abp.Domain.Repositories;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.Repositorios
{
    public interface IEstadoRepositorio : IRepository<Estado>
    {
        List<Estado> GetWithNombreEstado(int Id);

        /// <summary>
        ///     Se encarga de obtener la información de un estado a partir del nombre de estado y del nombre del parámetro
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro el  nombre del estado y el nombre del parámetro
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Parametros.Entidades.Estado"/> con la información del estado
        /// </return>
        Estado GetByNombreEstadoAndNombreParametro(string nombreEstado, string nombreParametro);
    }
}
