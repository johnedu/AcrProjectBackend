using Abp.EntityFramework;
using Bow.Afiliaciones.Entidades;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bow.Afiliaciones.Repositorios
{
    public class BeneficioRepositorio: BowRepositoryBase<Beneficio>, IBeneficioRepositorio
    {
        public BeneficioRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<Beneficio> GetWithTipoCategoria(int tipoId)
        {
            return GetAll().Where(b => b.TipoId == tipoId).Include(to => to.TipoBeneficio).ToList();
        }

    }
}
