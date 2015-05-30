using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Cartera.DTOs.OutputModels
{
    public class GetAllMonedaOutput : IOutputDto
    {
        public List<MonedaOutput> Monedas { get; set; }
    }
}
