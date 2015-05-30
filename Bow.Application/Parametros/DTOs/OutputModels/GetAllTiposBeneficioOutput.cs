using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.OutputModels
{
    public class GetAllTiposBeneficioOutput : IOutputDto
    {
        public List<TipoOutput> TiposGruposBeneficio { get; set; }
    }
}


