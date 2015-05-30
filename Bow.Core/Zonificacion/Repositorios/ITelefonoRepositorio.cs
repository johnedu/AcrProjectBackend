using Abp.Domain.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Repositorios
{
    public interface ITelefonoRepositorio : IRepository<Telefono>
    {
        /// <summary>
        /// Retorna el teléfono con la localidad precargada correspondiente al Id indicado
        /// </summary>
        /// <param name="id">Código de identificación del teléfono a consultar</param>
        /// <returns>Teléfono correspondiente al Id indicado</returns>
        Telefono GetWithLocalidad(int id);

        /// <summary>
        /// Retorna el teléfono con la localidad, el departamento y el tipo precargados del Id indicado
        /// </summary>
        /// <param name="id">Código de identificación del teléfono a consultar</param>
        /// <returns>Teléfono correspondiente al Id indicado</returns>
        Telefono GetWithLocalidadAndDepartamentoandTipo(int id);

        /// <summary>
        /// Retorna el teléfono con la localidad, el departamento y el tipo precargados correspondientes
        /// al número telefónico indicado
        /// </summary>
        /// <param name="numero">Número del teléfono a consultar</param>
        /// <param name="extension">Extensión del teléfono a consultar</param>
        /// <returns>Teléfono correspondiente al Id indicado</returns>
        Telefono GetWithLocalidadAndDepartamentoandTipo(string numero, string extension);
    }
}