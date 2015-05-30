using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalOutput : IOutputDto
    {
        public List<BeneficioPlanExequialByTipoOutput> Beneficios { get; set; }
    }
}
