using Abp.Domain.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Repositorios
{
    public interface IEmpresaContactoRepositorio : IRepository<EmpresaContacto>
    {
        /// <summary>
        ///     Obtiene una lista de contactos a partir del id de la empresa
        /// </summary>
        /// <param name="EmpresaId">Id de la empresa a consultar los tipos de contacto registrados</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Empresas.Entidades.EmpresaContacto"/> con la información del contacto y tipo</returns>
        List<EmpresaContacto> GetAllContactosEmpresaWithTipo(int EmpresaId);
    }
}
