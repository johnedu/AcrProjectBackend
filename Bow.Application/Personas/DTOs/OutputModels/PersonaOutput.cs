using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class PersonaOutput : EntityDto
    {
        public string TipoDocumentoNombre { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaNacimiento { get; set; }

        public string PaisNombre { get; set; }
        public string nombreCompleto { get; set; }
        public string Edad { get; set; }

       
    }
}