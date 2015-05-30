using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class SavePersonaProspectoInput : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int PaisId { get; set; }
        public DateTime? FechaNacimiento { get; set; }
       
        public string Genero { get; set; }
        public int TipoProfesionId { get; set; }
        public int TipoEstadoCivilId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaUltActualizacion { get; set; }

    }
}
