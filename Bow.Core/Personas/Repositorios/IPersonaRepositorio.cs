using Abp.Domain.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Repositorios
{
    public interface IPersonaRepositorio : IRepository<Persona>
    {
         /// <summary>
        /// Obtiene una Lista de las personas que contienen el número de documento indicado
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// <returns></returns>
        List<Persona> GetWithTipoDocumentoAndPaisAndDepartamentoByDocumento(string numeroDocumento);

        /// <summary>
        /// Obtiene una Lista de las personas con homónimo en su nombre completo con o sin numero de documento
        /// </summary>
        /// <param name="nombre, apellido1, apellido2, paisId"></param>
        /// <returns></returns>
        //List<Persona> GetWithTipoDocumentoAndPaisAndDepartamentoByDatosBasicosHomonimoConSinDocumento(string nombre, string apellido1, string apellido2, int paisId);
        List<Persona> GetWithTipoDocumentoAndDepartamentoByDatosBasicosHomonimoConSinDocumento(string nombre, string apellido1, string apellido2);

        /// <summary>
        /// Obtiene una persona con tipo de profesion
        /// </summary>
        /// <param name="personaId"></param>
        /// <returns></returns>
        Persona GetWithTipoProfesion(int personaId);

        ///// <summary>
        ///// Obtiene el filtro de buscador de personas
        ///// </summary>
        ///// <param name="personaId"></param>
        ///// <returns></returns>
        //List<Persona> GetAllPersonasByFilter(string query);
    }
}
