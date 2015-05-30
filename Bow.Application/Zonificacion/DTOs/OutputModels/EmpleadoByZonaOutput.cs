using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class EmpleadoByZonaOutput : EntityDto
    {
        public int EmpleadoId { get; set; }
        public string NombreEmpleado { get; set; }
        public int TipoId { get; set; }
        public string TipoNombre { get; set; }
        public int ZonaId { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public string FechaAsignacion { get; set; }
        public string FechaRetiro { get; set; }
    }
}

