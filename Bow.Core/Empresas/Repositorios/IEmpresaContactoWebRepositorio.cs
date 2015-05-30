using Abp.Domain.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Repositorios
{
    public interface IEmpresaContactoWebRepositorio : IRepository<EmpresaContactoWeb>
    {
        /// <summary>
        ///     Obtiene una lista de contactos web a partir del id de la empresa
        /// </summary>
        /// <param name="EmpresaId">Id de la empresa a consultar los tipos de contacto web registrados</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Empresas.Entidades.EmpresaContactoWeb"/> con la información del contacto web</returns>
        List<EmpresaContactoWeb> GetAllContactosWebWithTipo(int EmpresaId);
    }
}
