using Abp.EntityFramework;
using Bow.Afiliaciones.Entidades;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Repositorios
{
    public class ProspectoRepositorio: BowRepositoryBase<Prospecto>, IProspectoRepositorio
    {
        public ProspectoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }


    }
}
