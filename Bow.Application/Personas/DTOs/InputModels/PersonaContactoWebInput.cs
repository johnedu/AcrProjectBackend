using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class PersonaContactoWebInput : IInputDto
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public string Identificador { get; set; }
        public string MedioContactoNombre { get; set; }
        public int TipoId { get; set; }
        public string TipoCambio { get; set; }
    }
}
