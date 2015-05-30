using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetBeneficiosPlanOutput : EntityDto, IOutputDto
    {
        public int ValorPlan { get; set; }

        public List<TipoBeneficioPropioPlanExequialOutput> Propios { get; set; }
        public List<TipoBeneficioAdicionalPlanExequialOutput> Adicionales { get; set; }

    }
}
