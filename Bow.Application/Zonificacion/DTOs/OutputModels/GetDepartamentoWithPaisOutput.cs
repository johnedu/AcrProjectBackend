using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class GetDepartamentoWithPaisOutput : IOutputDto
    {
        public int IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; }
        public string IndicativoDepartamento { get; set; }
        public string NombrePais { get; set; }
        public string IndicativoPais { get; set; }
        public int IdPais { get; set; }
    }
}
