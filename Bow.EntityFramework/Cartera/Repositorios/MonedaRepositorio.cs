using Bow.EntityFramework.Repositories;
using Bow.Cartera.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Cartera.Repositorios
{
    public class MonedaRepositorio : BowRepositoryBase<Moneda>, IMonedaRepositorio
    {
        public MonedaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }
    }
}
