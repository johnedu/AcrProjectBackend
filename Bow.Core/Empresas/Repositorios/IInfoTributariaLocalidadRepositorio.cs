using Abp.Domain.Repositories;
using Bow.Empresas.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Repositorios
{
    public interface IInfoTributariaLocalidadRepositorio : IRepository<InfoTributariaLocalidad>
    {
        /// <summary>
        ///     Obtiene una lista de localidades con departamento y pais a partir del id de información tributaria
        /// </summary>
        /// <param name="Id">Código de la información tributaria a consultar las localidades registradas</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Zonificacion.Entidades.Localidad"/> con la información de la localidad</returns>
        List<Localidad> GetAllWithLocalidadAndDepartamentoAndPaisByInfoTributaria(int Id);

        /// <summary>
        ///     Obtiene una lista de localidades a partir del id de información tributaria y el id de un pais
        /// </summary>
        /// <param name="infoTributariaId">Código de la información tributaria a consultar las localidades registradas</param>
        /// <param name="paisId">Código del pais a consultar las localidades registradas</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Zonificacion.Entidades.Localidad"/> con la información de la localidad</returns>
        List<Localidad> GetAllWithLocalidadByInfoTributariaAndPais(int infoTributariaId, int paisId);

        /// <summary>
        ///     Obtiene una lista de localidades a partir del id de información tributaria y el id de un departamento
        /// </summary>
        /// <param name="infoTributariaId">Código de la información tributaria a consultar las localidades registradas</param>
        /// <param name="deptoId">Código del departamento a consultar las localidades registradas</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Zonificacion.Entidades.Localidad"/> con la información de la localidad</returns>
        List<Localidad> GetAllWithLocalidadByInfoTributariaAndDepartamento(int infoTributariaId, int deptoId);

        /// <summary>
        ///     Obtiene una lista de paises a partir del id de información tributaria
        /// </summary>
        /// <param name="infoTributariaId">Código de la información tributaria a consultar los paises registrados</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Zonificacion.Entidades.Pais"/> con la información del pais</returns>
        List<Pais> GetAllPaisesByInfoTributaria(int infoTributariaId);

        /// <summary>
        ///     Obtiene una lista de departamentos a partir del id de información tributaria
        /// </summary>
        /// <param name="infoTributariaId">Código de la información tributaria a consultar los departamentos registrados</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Zonificacion.Entidades.Departamento"/> con la información del departamento</returns>
        List<Departamento> GetAllDepartamentosByInfoTributaria(int infoTributariaId);

        /// <summary>
        ///     Obtiene una lista de información tributaria en estado vigente a partir del id de la localidad
        /// </summary>
        /// <param name="LocalidadId">Código de la localidad a consultar las informaciones tributarias registrados</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Empresas.Entidades.InfoTributaria"/> con la información de la información tributaria</returns>
        List<InfoTributaria> GetAllInfoTributariaActivasByLocalidad(int LocalidadId);
    }
}
