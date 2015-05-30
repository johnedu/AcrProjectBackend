using Bow.EntityFramework.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Zonificacion.Repositorios
{
    public class DepartamentoRepositorio : BowRepositoryBase<Departamento>, IDepartamentoRepositorio
    {
        public DepartamentoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public Departamento GetWithPais(int Id)
        {
            return GetAll().Where(d => d.Id == Id).Include(d => d.PaisDepartamento).FirstOrDefault();
        }

    }
}
