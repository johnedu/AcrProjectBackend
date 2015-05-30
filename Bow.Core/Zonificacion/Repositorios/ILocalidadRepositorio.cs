using Abp.Domain.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Repositorios
{
    public interface ILocalidadRepositorio : IRepository<Localidad>
    {
        /// <summary>
        ///     Se encarga de obtener la información de una localidad con la información del departamento y el pais
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro el id de la localidad
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Zonificacion.Entidades.Localidad"/>
        /// </return>
        Localidad GetWithDepartamentoAndPais(int Id);

        /// <summary>
        ///     Se encarga de obtener el listado de localidades con la información del departamento y el pais
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna una lista de objetos <see cref="Bow.Zonificacion.Entidades.Localidad"/>
        /// </return>
        List<Localidad> GetAllWithDepartamentoAndPais();

        /// <summary>
        ///     Se encarga de obtener el listado de localidades con la información del departamento y el pais por un pais específico
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna una lista de objetos <see cref="Bow.Zonificacion.Entidades.Localidad"/>
        /// </return>
        List<Localidad> GetAllWithDepartamentoAndPaisByPais(int PaisId);

        /// <summary>
        ///     Se encarga de obtener el listado de localidades con la información del departamento y el pais por un departamento específico
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna una lista de objetos <see cref="Bow.Zonificacion.Entidades.Localidad"/>
        /// </return>
        List<Localidad> GetAllWithDepartamentoAndPaisByDepartamento(int DepartamentoId);
    }
}