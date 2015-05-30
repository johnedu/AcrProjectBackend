using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.OutputModels
{
    public class EstadoWithNombreEstadoOutput : EntityDto
    {
        public string Motivo { get; set; }
        public int ParametroId { get; set; }
        public string ParametroNombre { get; set; }
        public int EstadoNombreId { get; set; }
        public string EstadoNombreNombre { get; set; }
        public string EstadoNombreAbreviacion { get; set; }
    }
}
