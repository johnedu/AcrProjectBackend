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
   public class TipoDocumentoPersonaRepositorio : BowRepositoryBase<TipoDocumentoPersona>, ITipoDocumentoPersonaRepositorio
    {
       public TipoDocumentoPersonaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

       public List<TipoDocumentoPersona> GetAllTiposDocumentoWithPais()
       {
           return GetAll().Include(td => td.TipoDocumentoPersonaPais).OrderBy(p => p.TipoDocumentoPersonaPais.Nombre).ThenBy(p => p.Nombre).ToList();
       }
    }
}

