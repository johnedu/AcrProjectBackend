using Bow.EntityFramework.Repositories;
using Bow.Afiliaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Afiliaciones.Repositorios
{
    public class GrupoInformalRepositorio : BowRepositoryBase<GrupoInformal>, IGrupoInformalRepositorio
    {
        public GrupoInformalRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }
    }
}
