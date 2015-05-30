using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class TipoBeneficioPropioPlanExequialOutput: EntityDto
    {
        public string Nombre { get; set; }
        public bool Seleccionado { get; set; }
    }
}
