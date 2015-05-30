using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class UpdatePlanExequialRecaudoMasivoInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "planExequialRecaudoMasivo_update_id_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "planExequialRecaudoMasivo_update_planExequialId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PlanExequialId { get; set; }

        [Required(ErrorMessageResourceName = "planExequialRecaudoMasivo_update_recaudoMasivoId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int RecaudoMasivoId { get; set; }

        [Required(ErrorMessageResourceName = "planExequialRecaudoMasivo_update_esObligatorio_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public bool EsObligatorio { get; set; }

    }
}