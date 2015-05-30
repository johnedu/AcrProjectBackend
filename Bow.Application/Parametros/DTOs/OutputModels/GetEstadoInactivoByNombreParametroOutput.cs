using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.OutputModels
{
    public class GetEstadoInactivoByNombreParametroOutput : IOutputDto
    {
        public int EstadoId { get; set; }
    }
}

