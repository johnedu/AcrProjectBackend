using Bow.EntityFramework.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Personas.Repositorios
{
    public class PersonaTelefonoRepositorio : BowRepositoryBase<PersonaTelefono>, IPersonaTelefonoRepositorio
    {
        public PersonaTelefonoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<PersonaTelefono> GetWithTelefonoWithLocalidadAndEstadoByPersona(int personaId)
        {
            return GetAll().Where(p => p.PersonaId == personaId)
              .Include(to => to.TelefonoPersonaTelefono.LocalidadTelefono.DepartamentoLocalidad)
              .Include(to => to.TipoUbicacion)
              .Include(to => to.Estado).ToList();
        }
    }
}
