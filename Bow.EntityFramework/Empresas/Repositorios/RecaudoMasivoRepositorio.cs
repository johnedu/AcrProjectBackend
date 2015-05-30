using Bow.EntityFramework.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Empresas.Repositorios
{
    public class RecaudoMasivoRepositorio : BowRepositoryBase<RecaudoMasivo>, IRecaudoMasivoRepositorio
    {
        public RecaudoMasivoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }
    }
}
