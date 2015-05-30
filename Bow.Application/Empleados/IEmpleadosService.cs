using Abp.Application.Services;
using Bow.Empleados.DTOs.InputModels;
using Bow.Empleados.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados
{
    public interface IEmpleadosService : IApplicationService
    {
        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Empleados.DTOs.InputModels.SaveEmpleadoInput"/> con la información del empleado
        /// para almacenarlo en la base de datos
        /// </summary>
        /// <returns></returns>
        void SaveEmpleado(SaveEmpleadoInput empleadoInput);

        /// <summary>
        ///     Se encarga de hacer un filtro de los empleados segun parámetros de entrada
        /// </summary>
        /// <param name="datosInput">
        ///     Ingresa un objeto <see cref="Bow.Application.Empleados.DTOs.InputModels.GetBuscadorEmpleadoOutput"/> con la información por las cuales se quiere filtrar
        /// </param>
        /// <returns>
        ///     Retorna un objeto <see cref="Bow.Application.Empleados.DTOs.OutputModels.GetBuscadorEmpleadoOutput"/> con la lista de los empleados que cumplen los criterios del filtro
        /// </returns>
        GetBuscadorEmpleadoOutput GetBuscadorEmpleado(GetBuscadorEmpleadoInput datosInput);

        /// <summary>
        ///     Se encarga de consultar los empleados en el sistema con la sucursal y la localidad de cada uno
        /// </summary>
        /// <returns>
        ///     Retorna un objeto <see cref="Bow.Application.Empleados.DTOs.OutputModels.GetEmpleadosWithSucursalAndLocalidadOutput"/> con la lista de los empleados con la sucursal y la localidad de cada uno
        /// </returns>
        GetEmpleadosWithSucursalAndLocalidadOutput GetEmpleadosWithSucursal();

        /// <summary>
        ///     Se encarga de consulta el empleado a partir del Id
        /// </summary>
        /// <param name="empleado">
        ///     Ingresa un objeto <see cref="Bow.Application.Empleados.DTOs.InputModels.GetEmpleadosByIdInput"/> con el Id del empleado
        /// </param>
        /// <returns>
        ///     Retorna un objeto <see cref="Bow.Application.Empleados.DTOs.OutputModels.GetEmpleadosByIdOutput"/> con la información del empleado
        /// </returns>
        GetEmpleadosByIdOutput GetEmpleadosById(GetEmpleadosByIdInput empleado);
    }
}
