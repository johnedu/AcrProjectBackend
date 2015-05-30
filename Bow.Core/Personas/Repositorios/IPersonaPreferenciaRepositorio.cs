using Abp.Domain.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Repositorios
{
    public interface IPersonaPreferenciaRepositorio : IRepository<PersonaPreferencia>
    {
        List<PersonaPreferencia> GetWithPreferencia(int personaId);
      
    }
}
