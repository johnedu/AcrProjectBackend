using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class GetPersonaEditarOutput : EntityDto
    {
        public bool TieneDocumento { get; set; }
        public int? TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int PaisId { get; set; }
        public string PaisNombre { get; set; }
        public string Indicativo { get; set; }
        public string FechaExpDocumento { get; set; }
        public bool TieneFechaNacimiento { get; set; }
        public string FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string CorreoElectronico { get; set; }
        public bool ContactarCorreo { get; set; }
        public bool ContactarSms { get; set; }
        public bool ContactarTelefono { get; set; }
        public int TipoProfesionId { get; set; }
        public string TipoProfesionNombre { get; set; }
        public int TipoEstadoCivilId { get; set; }
        public string TipoEstadoCivilNombre { get; set; }
        public bool Discapacitada { get; set; }
        public string FechaFallecimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaUltActualizacion { get; set; }
        public string Usuario { get; set; }
        public string Edad { get; set; }
    }
}
