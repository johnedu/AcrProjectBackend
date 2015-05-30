﻿using Abp.Domain.Repositories;
using Bow.Afiliaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Repositorios
{
    public interface IAfiliadoProspectoRepositorio : IRepository<AfiliadoProspecto>
    {
        List<AfiliadoProspecto> GetWithParentescoAndLocalidadByGestionProspecto(int gestionProspectoId);

        AfiliadoProspecto GetWithParentescoAndLocalidadByAfiliadoProspecto(int afiliadoProspectoId);
    }
}
