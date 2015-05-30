using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class GetZonaEmpleadoOutput : EntityDto
    {
        public int Codigo { get; set; }
        public int EmpleadoId { get; set; }
        public string NombreCompleto { get; set; }
        public int TipoId { get; set; }
        public string FechaAsignacion { get; set; }

        public DateTime FechaRetiroMaxima { get; set; }
        public DateTime FechaRetiroMinima { get; set; }

    }
}