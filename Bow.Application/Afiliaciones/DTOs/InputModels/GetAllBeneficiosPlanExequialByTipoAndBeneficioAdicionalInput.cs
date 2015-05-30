using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_getall_planExequialId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PlanExequialId { get; set; }

        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_getall_categoriaId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int TipoId { get; set; }

        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_getall_categoriaId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int BeneficioAdicionalId { get; set; }
    }
}