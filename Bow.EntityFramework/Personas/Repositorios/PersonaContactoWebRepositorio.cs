using Abp.EntityFramework;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bow.Personas.Repositorios
{
   public class PersonaContactoWebRepositorio : BowRepositoryBase<PersonaContactoWeb>, IPersonaContactoWebRepositorio
    {
       public PersonaContactoWebRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

       public List<PersonaContactoWeb> GetWithTipoMedioContacto(int personaId)
       {
           return GetAll().Where(p => p.PersonaId == personaId)
             .Include(pc => pc.TipoPersonaContactoWeb).ToList();
       }
    }
}
