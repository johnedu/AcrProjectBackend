using Bow.EntityFramework.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Zonificacion.Repositorios
{
    public class LocalidadRepositorio : BowRepositoryBase<Localidad>, ILocalidadRepositorio
    {
        public LocalidadRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public Localidad GetWithDepartamentoAndPais(int Id)
        {
            return GetAll().Where(l => l.Id == Id).Include(l => l.DepartamentoLocalidad.PaisDepartamento).FirstOrDefault();
        }

        public List<Localidad> GetAllWithDepartamentoAndPais()
        {
            return GetAll().Include(l => l.DepartamentoLocalidad.PaisDepartamento).OrderBy(l => l.Nombre).ToList();
        }

        public List<Localidad> GetAllWithDepartamentoAndPaisByPais(int PaisId)
        {
            return GetAll().Where(p => p.DepartamentoLocalidad.PaisId == PaisId).Include(l => l.DepartamentoLocalidad.PaisDepartamento).OrderBy(l => l.Nombre).ToList();
        }

        public List<Localidad> GetAllWithDepartamentoAndPaisByDepartamento(int DepartamentoId)
        {
            return GetAll().Where(d => d.DepartamentoId == DepartamentoId).Include(l => l.DepartamentoLocalidad.PaisDepartamento).OrderBy(l => l.Nombre).ToList();
        }
    }
}
