using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class SavePlanExequialRecaudoMasivoInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "planExequialRecaudoMasivo_save_planExequialId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PlanExequialId { get; set; }

        [Required(ErrorMessageResourceName = "planExequialRecaudoMasivo_save_recaudoMasivoId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int RecaudoMasivoId { get; set; }

        [Required(ErrorMessageResourceName = "planExequialRecaudoMasivo_save_esObligatorio_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public bool EsObligatorio { get; set; }

    }
}