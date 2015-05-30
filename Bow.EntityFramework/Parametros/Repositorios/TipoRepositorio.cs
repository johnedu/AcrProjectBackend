using Bow.EntityFramework.Repositories;
using Bow.Parametros.Entidades;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;


namespace Bow.Parametros.Repositorios
{
    public class TipoRepositorio : BowRepositoryBase<Tipo>, ITipoRepositorio
    {
        public TipoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<Tipo> GetAllTipos()
        {
            return GetAll().Include(l => l.ParametroTipo).ToList();
        }
    }
}
