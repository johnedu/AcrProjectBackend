using Abp.Domain.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Repositorios
{
    public interface ISucursalRepositorio : IRepository<Sucursal>
    {
        /// <summary>
        ///     Obtiene la lista de sucursales
        /// </summary>
        /// <param name="EmpresaOrganizacionId">Id de la empresa organización</param>
        /// <returns>Retorna una lista de objetos de <see cref="Bow.Application.Empresas.Entidades.Sucursal"/> con la información de la sucursal</returns>
        List<Sucursal> GetAllSucursalesWithTipoByEmpresa(int EmpresaOrganizacionId);

        /// <summary>
        ///     Obtiene la información de la sucursal con la información del tipo, estado y dirección
        /// </summary>
        /// <param name="EmpresaOrganizacionId">Id de la empresa organización</param>
        /// <param name="SucursalId">Id de la sucursal</param>
        /// <returns>Retorna un objeto de <see cref="Bow.Application.Empresas.Entidades.Sucursal"/> con la información de la sucursal</returns>
        Sucursal GetSucursalWithTipoAndEstadoAndDireccion(int EmpresaOrganizacionId, int SucursalId);

        /// <summary>
        ///     Obtiene la lista de todas las sucursales con empresa y organizacion
        /// </summary>
        /// <returns>Retorna una lista de objetos de <see cref="Bow.Application.Empresas.Entidades.Sucursal"/> con la información de las sucursales con empresa y organizacion</returns>
        List<Sucursal> GetAllSucursalesWithEmpresaAndOrganizacion();

        /// <summary>
        ///     Obtiene la información de la sucursal con la información de la empresa y la organización a partir del Id
        /// </summary>
        /// <param name="SucursalId">Id de la sucursal</param>
        /// <returns>Retorna un objeto de <see cref="Bow.Application.Empresas.Entidades.Sucursal"/> con la información de la sucursal</returns>
        Sucursal GetSucursalByIdWithEmpresaAndOrganizacion(int SucursalId);
    }
}
