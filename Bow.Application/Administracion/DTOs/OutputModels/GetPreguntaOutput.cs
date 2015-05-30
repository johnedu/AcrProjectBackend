using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class GetPreguntaOutput : EntityDto, IOutputDto
    {
        public string Texto { get; set; }
        public int JuegoId { get; set; }
        public int DimensionId { get; set; }
        public string Nivel { get; set; }
        public string Pista { get; set; }
        public bool EstadoActiva { get; set; }
    }
}
