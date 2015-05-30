using Abp.Domain.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Repositorios
{
    public interface ISucursalTelefonoRepositorio : IRepository<SucursalTelefono>
    {
        /// <summary>
        ///     Obtiene la lista de telefonos con su localidad de una sucursal específica
        /// </summary>
        /// <param name="SucursalId">Id de la sucursal a consultar</param>
        /// <returns> Retorna un objeto de <see cref="Bow.Application.Empresas.Entidades.SucursalTelefono"/> con la información del teléfono y la localidad</returns>
        List<SucursalTelefono> GetAllTelefonosWithLocalidad(int SucursalId);
    }
}
