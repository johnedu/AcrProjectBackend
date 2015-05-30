using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class SavePeriodosVentasInput : IInputDto
    {
        public string Prefijo { get; set; }
        public int PeriodoInicial { get; set; }
        public string FechaInicio { get; set; }
        public int Intervalo { get; set; }
        public string Tiempo { get; set; }
        public string FechaFin { get; set; }
    }
}
