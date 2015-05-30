using Abp.Domain.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Repositorios
{
    public interface IDireccionRepositorio : IRepository<Direccion>
    {
        Direccion GetWithLocalidadAndDepartamento(int barrioId, string nombre);

        Direccion GetWithLocalidadAndDepartamentoById(int Id);

        Direccion GetWithLocalidadAndDepartamentoByNombreAndCodeZip(string nombre, string codeZip);


    }
}
