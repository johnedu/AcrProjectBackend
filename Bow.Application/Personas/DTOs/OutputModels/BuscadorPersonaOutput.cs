using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class BuscadorPersonaOutput : EntityDto
    {
        public int Id { get; set; }
        public bool TieneDocumento { get; set; }
        public string TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaExpDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public bool TieneFechaNacimiento { get; set; }
        public string FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string CorreoElectronico { get; set; }
        public bool ContactarCorreo { get; set; }
        public bool ContactarSms { get; set; }
        public bool ContactarTelefono { get; set; }
        public string FechaIngreso { get; set; }
        public int TipoProfesionId { get; set; }
        public int TipoEstadoCivilId { get; set; }
        public bool Discapacitada { get; set; }
        public string FechaFallecimiento { get; set; }
        public int PaisId { get; set; }
        public string NombreCompleto { get; set; }

        public string LocalidadDepartamento { get; set; }
    }
}
