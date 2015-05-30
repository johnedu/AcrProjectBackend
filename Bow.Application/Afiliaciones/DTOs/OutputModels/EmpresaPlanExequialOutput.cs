using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class EmpresaPlanExequialOutput : IOutputDto
    {
        public int EmpresaOrganizacionId { get; set; }
        public string EmpresaNombre { get; set; }
        public int SucursalesAsignadas { get; set; }
        public int SucursalesNoAsignadas { get; set; }
    }
}
