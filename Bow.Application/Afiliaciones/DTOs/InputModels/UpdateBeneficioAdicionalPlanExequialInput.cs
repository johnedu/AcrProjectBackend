using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class UpdateBeneficioAdicionalPlanExequialInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_update_id_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_update_valor_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int Valor { get; set; }

        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_update_cantidadAsignables_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int Asignables { get; set; }

        public string FechaIngreso { get; set; }
        public string FechaCancelacion { get; set; }

        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_update_planExequialId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PlanExequialId { get; set; }

        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_update_beneficioId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int BeneficioId { get; set; }

        public int? EstadoId { get; set; }

        public int? BeneficioPlanExequialId { get; set; }

    }
}