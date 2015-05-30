using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class EmpleadoInput : EntityDto
    {
        public string FechaIngreso { get; set; }
        public string FechaCancelacion { get; set; }
        public int GrupoInformalId { get; set; }
        public int EmpleadoId { get; set; }
        public int EstadoId { get; set; }
    }
}
