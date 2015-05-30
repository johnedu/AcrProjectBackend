using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class PersonaDireccionInput : IInputDto
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public int DireccionId { get; set; }
        public int TipoUbicacionId { get; set; }
        public int EstadoId { get; set; }
        public bool NombreEstado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public string UsuarioCancelacion { get; set; }
        public string TipoCambio { get; set; }
    }
}