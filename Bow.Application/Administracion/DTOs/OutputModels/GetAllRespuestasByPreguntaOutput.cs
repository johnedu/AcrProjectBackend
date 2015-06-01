using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class GetAllRespuestasByPreguntaOutput : IOutputDto
    {
        public List<RespuestasByPreguntaOutput> Respuestas { get; set; }
        public int Comodines5050 { get; set; }
    }
}
