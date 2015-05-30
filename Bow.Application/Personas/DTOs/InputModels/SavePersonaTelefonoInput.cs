using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class SavePersonaTelefonoInput : IInputDto
    {
        public int PersonaId { get; set; }
        public List<PersonaTelefonoInput> PersonaTelefonos { get; set; }
    }
}
