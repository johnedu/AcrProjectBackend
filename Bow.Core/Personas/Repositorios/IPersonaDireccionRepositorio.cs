using Abp.Domain.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Repositorios
{
    public interface IPersonaDireccionRepositorio : IRepository<PersonaDireccion>
    {
        /// <summary>
        /// Obtiene una Lista de los números de telefono con localidad de la persona.
        /// </summary>
        /// <param name="personaId"></param>
        /// <returns></returns>
        List<PersonaDireccion> GetWithDireccionWithLocalidadAndEstadoByPersona(int personaId);
    }
}
