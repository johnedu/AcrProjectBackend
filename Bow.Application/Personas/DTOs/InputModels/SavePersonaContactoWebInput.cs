using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class SavePersonaContactoWebInput : IInputDto
    {
        public List<PersonaContactoWebInput> PersonaContactoWeb { get; set; }
    }
}