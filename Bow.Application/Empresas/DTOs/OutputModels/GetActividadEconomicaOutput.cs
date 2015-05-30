using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class GetActividadEconomicaOutput : EntityDto
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
    }
}