using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class TelefonoPersonaOutput : EntityDto
    {
        public int PersonaId { get; set; }
        public int TelefonoId { get; set; }
        public string TelefonoNumero { get; set; }
        public string TelefonoTipo { get; set; }
        public string NombreLocalidad { get; set; }
        public int TipoUbicacionId { get; set; }
        public string TipoUbicacionNombre { get; set; }
        public int EstadoId { get; set; }
        public bool NombreEstado { get; set; }
        public string FechaIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public string FechaCancelacion { get; set; }
        public string UsuarioCancelacion { get; set; }
        public string TipoCambio { get; set; }
    }
}