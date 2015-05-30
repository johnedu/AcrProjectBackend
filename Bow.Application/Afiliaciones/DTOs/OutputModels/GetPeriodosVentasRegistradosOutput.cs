using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetPeriodosVentasRegistradosOutput : IOutputDto
    {
        public DateTime? FechaInicioMinima { get; set; }
        public DateTime? FechaInicioMaxima { get; set; }
        public DateTime? FechaInicioProxima { get; set; }
        
        public List<PeriodoVentaOutPut> PeriodosRegistrados { get; set; }
    }
}