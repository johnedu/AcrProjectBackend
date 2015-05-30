using Abp.EntityFramework;
using Bow.Empleados.Entidades;
using Bow.Empleados.Repositorios;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bow.Empleados.Repositorios
{
    public class EmpleadoRepositorio : BowRepositoryBase<Empleado>, IEmpleadoRepositorio
    {
        public EmpleadoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<Empleado> GetWithPersona()
        {
            return GetAll().Include(to => to.PersonaEmpleado).ToList();
        }

        public List<Empleado> GetWithSucursal()
        {
            return GetAll().Include(to => to.PersonaEmpleado)
                .Include(to => to.SucursalEmpleado.EmpresaOrganizacion.EmpresaEmpresaOrganizacion)
                .Include(to => to.SucursalEmpleado.EmpresaOrganizacion.OrganizacionEmpresaOrganizacion)
                .Include(to => to.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.PaisDepartamento)
                .ToList();
        }

        public Empleado GetByIdWithSucursal(int EmpleadoId)
        {
            return GetAll().Where(e => e.Id == EmpleadoId).Include(to => to.PersonaEmpleado)
                .Include(to => to.SucursalEmpleado.EmpresaOrganizacion.EmpresaEmpresaOrganizacion)
                .Include(to => to.SucursalEmpleado.EmpresaOrganizacion.OrganizacionEmpresaOrganizacion)
                .Include(to => to.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.PaisDepartamento).FirstOrDefault();
        }
    }
}
