using Abp.Domain.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Repositorios
{
    public interface IEmpresaTelefonoRepositorio : IRepository<EmpresaTelefono>
    {
        /// <summary>
        ///     Obtiene la lista de telefonos con su localidad de una empresa específica
        /// </summary>
        /// <param name="EmpresaId">Id de la empresa a consultar</param>
        /// <returns> Retorna un objeto de <see cref="Bow.Application.Empresas.Entidades.EmpresaTelefono"/> con la información del teléfono y la localidad</returns>
        List<EmpresaTelefono> GetAllTelefonosWithLocalidad(int EmpresaId);
    }
}
