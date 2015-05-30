using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class RespuestasByPreguntaOutput : EntityDto, IOutputDto
    {
        public string Texto { get; set; }
        public bool Comodin50_50 { get; set; }
        public bool RespuestaVerdadera { get; set; }
        public int PreguntaId { get; set; }
    }
}
