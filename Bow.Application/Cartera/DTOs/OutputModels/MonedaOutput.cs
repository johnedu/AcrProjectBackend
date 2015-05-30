using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Cartera.DTOs.OutputModels
{
    public class MonedaOutput : EntityDto , IOutputDto
    {
        public string Nombre { get; set; }
        public string Simbolo { get; set; }
    }
}
