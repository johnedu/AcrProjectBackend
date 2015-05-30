using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetPlanExequialRecaudoMasivoOutput : EntityDto, IOutputDto
    {
        public int PlanExequialId { get; set; }
        public int RecaudoMasivoId { get; set; }
        public bool EsObligatorio { get; set; }
        public string RecaudoMasivoNombre { get; set; }
        public string RecaudoMasivoClave { get; set; }
    }
}
