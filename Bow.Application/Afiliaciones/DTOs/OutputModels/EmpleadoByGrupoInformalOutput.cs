using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class EmpleadoByGrupoInformalOutput : EntityDto, IOutputDto
    {
        public int Codigo { get; set; }
        public string NombreCompleto { get; set; }
        public string Localidad { get; set; }
    }
}
