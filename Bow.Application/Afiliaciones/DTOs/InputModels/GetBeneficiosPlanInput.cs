using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class GetBeneficiosPlanInput : EntityDto
    {
        public int PlanExequialId { get; set; }
    }
}
