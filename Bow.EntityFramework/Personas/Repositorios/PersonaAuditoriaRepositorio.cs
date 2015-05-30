using Abp.EntityFramework;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Repositorios
{
    public class PersonaAuditoriaRepositorio: BowRepositoryBase<PersonaAuditoria>, IPersonaAuditoriaRepositorio
    {
        public PersonaAuditoriaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
