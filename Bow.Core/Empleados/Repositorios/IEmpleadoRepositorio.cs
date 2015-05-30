using Abp.Domain.Repositories;
using Bow.Empleados.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados.Repositorios
{
    public interface IEmpleadoRepositorio : IRepository<Empleado>
    {
        List<Empleado> GetWithPersona();

        List<Empleado> GetWithSucursal();

        Empleado GetByIdWithSucursal(int EmpleadoId);
    }
}
