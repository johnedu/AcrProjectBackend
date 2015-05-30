using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class SaveGestionProspectoOutput: EntityDto
    {
        public int ProspectoId { get; set; }
        public int EmpleadoId { get; set; }
        public int? PersonaId { get; set; }
        public string EmpresaAfiliada { get; set; }
    }
}
