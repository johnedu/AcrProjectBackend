using Abp.Domain.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Repositorios
{
    public interface IZonaRepositorio : IRepository<Zona>
    {
        List<Zona> GetAllListWithZonaByLocalidad(int localidadId);

        Zona GetWithTipo(int id);

        List<Zona> GetAllListWithZonaByLocalidadAndTipoZona(int localidadId, int zonaId);
    }
}
