using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class GetEmpleadosInactivosByZonaOutput : IOutputDto
    {
        public List<EmpleadoByZonaOutput> EmpleadosInactivos { get; set; }
    }
}
