﻿using Abp.Domain.Repositories;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Repositorios
{
    public interface IZonaBarrioRepositorio : IRepository<ZonaBarrio>
    {
        List<ZonaBarrio> GetAllListWithBarriosByZona(int zonaId);

        List<ZonaBarrio> GetAllListWithZonaBarrioByZonaAndLocalidad(int localidadId, int zonaId);
    }
}