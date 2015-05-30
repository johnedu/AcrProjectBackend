using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class PersonaEditadaAuditoriaInput : EntityDto
    {
        public int Id { get; set; }

        public string TieneDocumento { get; set; }
        public int? TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime? FechaExpDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string TieneFechaNacimiento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string CorreoElectronico { get; set; }
        public string ContactarSms { get; set; }
        public string ContactarCorreo { get; set; }
        public string ContactarTelefono { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int TipoProfesionId { get; set; }
        public int TipoEstadoCivilId { get; set; }
        public string Discapacitada { get; set; }
        public DateTime? FechaFallecimiento { get; set; }
        public int PaisId { get; set; }
        public DateTime FechaUltActualizacion { get; set; }
        public string Usuario { get; set; }

    }
}
