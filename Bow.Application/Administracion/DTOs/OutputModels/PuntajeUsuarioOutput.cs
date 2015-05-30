using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class PuntajeUsuarioOutput : EntityDto, IOutputDto
    {
        public string Dimension { get; set; }
        public string Juego { get; set; }
        public string Puntaje { get; set; }
    }
}
