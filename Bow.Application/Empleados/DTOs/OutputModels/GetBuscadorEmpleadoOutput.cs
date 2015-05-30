using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados.DTOs.OutputModels
{
    public class GetBuscadorEmpleadoOutput : IOutputDto
    {
        public List<BuscadorEmpleadoOutput> Empleados { get; set; }

    }
}
