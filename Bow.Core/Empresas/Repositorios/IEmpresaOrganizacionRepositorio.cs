using Abp.Domain.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Repositorios
{
    public interface IEmpresaOrganizacionRepositorio : IRepository<EmpresaOrganizacion>
    {
        /// <summary>
        ///     Obtiene una lista de empresas a partir del id de la organizacion
        /// </summary>
        /// <param name="OrganizacionId">Código de la organización a consultar las empresas registradas</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Empresas.Entidades.EmpresaOrganizacion"/> con la información de la empresa</returns>
        List<EmpresaOrganizacion> GetAllEmpresasByOrganizacion(int OrganizacionId);

        /// <summary>
        ///     Obtiene información de la organización y de la empresa asociada
        /// </summary>
        /// <param name="OrganizacionId">Código de la organización a consultar</param>
        /// <returns>Retorna un objeto de <see cref="Bow.Application.Empresas.Entidades.EmpresaOrganizacion"/> con la información de la organización y la empresa</returns>
        EmpresaOrganizacion GetEmpresaWithOrganizacion(int OrganizacionId, int EmpresaId);
    }
}
