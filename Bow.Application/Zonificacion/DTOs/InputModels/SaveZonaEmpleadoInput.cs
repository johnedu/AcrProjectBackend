using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SaveZonaEmpleadoInput : IInputDto
    {
        public int ZonaId { get; set; }
        public int EmpleadoId { get; set; }
        public int TipoId { get; set; }
        public int EstadoId { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public DateTime? FechaRetiro { get; set; }

    }
}

