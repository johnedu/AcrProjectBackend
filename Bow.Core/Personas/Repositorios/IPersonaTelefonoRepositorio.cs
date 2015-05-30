using Abp.Domain.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Repositorios
{
    public interface IPersonaTelefonoRepositorio : IRepository<PersonaTelefono>
    {
        /// <summary>
        /// Obtiene una Lista de los números de telefono con localidad de la persona.
        /// </summary>
        /// <param name="personaId"></param>
        /// <returns></returns>
        List<PersonaTelefono> GetWithTelefonoWithLocalidadAndEstadoByPersona(int personaId);
    }
}
