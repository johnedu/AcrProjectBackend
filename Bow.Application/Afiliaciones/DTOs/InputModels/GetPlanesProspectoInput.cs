using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class GetPlanesProspectoInput : EntityDto
    {
        public int SucursalId { get; set; }
        public List<PlanProspectoInput> Parentescos { get; set; }
    }
}
