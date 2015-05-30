using Abp.Domain.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Repositorios
{
    public interface IEmpresaInfoTributariaRepositorio : IRepository<EmpresaInfoTributaria>
    {
        /// <summary>
        ///     Obtiene una lista de información tributaria que no estén canceladas a partir del id de la empresa
        /// </summary>
        /// <param name="EmpresaId">Código de la empresa a consultar las informaciones tributarias registradas</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Empresas.Entidades.InfoTributaria"/> con la información de la información tributaria</returns>
        List<InfoTributaria> GetAllInfoTributariaByEmpresa(int EmpresaId);

        /// <summary>
        ///     Obtiene la lista de opciones de información tributaria a partir del id de la empresa
        /// </summary>
        /// <param name="EmpresaId">Código de la empresa a consultar las opciones de información tributarias registradas</param>
        /// <returns>Retorna una lista de <see cref="Bow.Application.Empresas.Entidades.EmpresaInfoTributaria"/> con la información de las opciones de información tributaria</returns>
        List<EmpresaInfoTributaria> GetAllOpcionesInfoTributariaByEmpresa(int EmpresaId);
    }
}
