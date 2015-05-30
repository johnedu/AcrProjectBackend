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
   public class PersonaDireccionRepositorio: BowRepositoryBase<PersonaDireccion>, IPersonaDireccionRepositorio
    {
       public PersonaDireccionRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<PersonaDireccion> GetWithDireccionWithLocalidadAndEstadoByPersona(int personaId)
        {
            return GetAll().Where(p => p.PersonaId == personaId)
              .Include(to => to.DireccionPersonaDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad)
              .Include(to => to.TipoUbicacion)
              .Include(to => to.Estado).ToList();
        }
    }
}
