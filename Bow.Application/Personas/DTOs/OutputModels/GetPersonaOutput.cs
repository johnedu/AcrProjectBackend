using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class GetPersonaOutput : EntityDto
    {
        public string NumeroDocumento { get; set; }
        public string NombreCompleto { get; set; }
    }
}
