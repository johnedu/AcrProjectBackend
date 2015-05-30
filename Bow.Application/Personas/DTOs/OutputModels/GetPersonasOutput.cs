using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class GetPersonasOutput : EntityDto
    {
        //public string ValidarNumeroDocumento { get; set; }
        public bool PuedeSeleccionarPersona { get; set; }
        public List<PersonaOutput> Personas { get; set; }

    }
}
