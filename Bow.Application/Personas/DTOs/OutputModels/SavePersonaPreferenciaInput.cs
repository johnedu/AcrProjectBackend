using Abp.Application.Services.Dto;
using Bow.Personas.DTOs.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class SavePersonaPreferenciaInput : IOutputDto
    {
        public List<PersonaPreferenciaInput> PreferenciasPersona { get; set; }
    }
}