using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class PeriodoVentaInput : EntityDto
    {
        //public string Prefijo { get; set; }
        //public int PeriodoInicial { get; set; }
        //public int Intervalo { get; set; }
        //public string Tiempo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Nombre { get; set; }

    }
}