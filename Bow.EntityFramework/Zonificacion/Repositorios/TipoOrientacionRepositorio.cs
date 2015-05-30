using Abp.EntityFramework;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Repositorios
{
    public class TipoOrientacionRepositorio : BowRepositoryBase<TipoOrientacion>, ITipoOrientacionRepositorio
    {
        public TipoOrientacionRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public TipoOrientacion GetByNombre(string nombreTipoOrientacion)
        {
            return GetAll().Where(to => to.Nombre.ToLower() == nombreTipoOrientacion.ToLower()).FirstOrDefault();
        }
    }
}
