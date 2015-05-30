using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.InputModels
{
    public class GetEstadoByNombreEstadoAndNombreParametroInput : EntityDto
    {
        public string NombreEstado { get; set; }
        public string NombreParametro { get; set; }
    }
}

