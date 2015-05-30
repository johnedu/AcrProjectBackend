using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
   public class GetEmpleadosActivosByZonaOutput : IOutputDto
    {
       public List<EmpleadoByZonaOutput> EmpleadosActivos { get; set; }
    }
}