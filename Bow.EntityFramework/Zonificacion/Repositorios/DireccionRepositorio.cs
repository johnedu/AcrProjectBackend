using Abp.EntityFramework;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bow.Zonificacion.Repositorios
{
    public class DireccionRepositorio : BowRepositoryBase<Direccion>, IDireccionRepositorio
    {
        public DireccionRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public Direccion GetWithLocalidadAndDepartamento(int barrioId, string nombre)
        {
            return GetAll().Where(d => d.BarrioId == barrioId && d.Nombre.ToLower() == nombre.ToLower()).Include(to => to.BarrioDireccion.Localidad.DepartamentoLocalidad).FirstOrDefault();
        }

        public Direccion GetWithLocalidadAndDepartamentoById(int Id)
        {
            return GetAll().Where(d => d.Id == Id).Include(to => to.BarrioDireccion.Localidad.DepartamentoLocalidad).FirstOrDefault();
        }

        public Direccion GetWithLocalidadAndDepartamentoByNombreAndCodeZip(string nombre, string codeZip)
        {
            return GetAll().Where(d => d.Nombre.ToLower() == nombre.ToLower() && d.ZipCode.ToLower() == codeZip.ToLower()).Include(to => to.BarrioDireccion.Localidad.DepartamentoLocalidad).FirstOrDefault();
        }

    }
}