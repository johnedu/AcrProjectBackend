using Bow.EntityFramework.Repositories;
using Bow.Afiliaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;
using System.Data.Entity;
using EntityFramework.Extensions;

namespace Bow.Afiliaciones.Repositorios
{
    public class ParentescoRepositorio : BowRepositoryBase<Parentesco>, IParentescoRepositorio
    {
        public ParentescoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public void MoverPosicionParentesco(int posicion, int cantidad)
        {
            GetAll().Where(p => p.Posicion >= posicion).Update(p2 => new Parentesco { Posicion = p2.Posicion + cantidad });
        }

    }
}
