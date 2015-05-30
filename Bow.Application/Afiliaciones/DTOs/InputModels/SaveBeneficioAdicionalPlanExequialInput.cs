using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class SaveBeneficioAdicionalPlanExequialInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_save_planExequialId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PlanExequialId { get; set; }

        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_save_beneficioId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int BeneficioId { get; set; }

        public string FechaIngreso { get; set; }

        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_save_cantidadAsignables_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int Asignables { get; set; }
        
        public string FechaCancelacion { get; set; }

        [Required(ErrorMessageResourceName = "beneficiosPlanExequial_save_valor_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int Valor { get; set; }

        public int? EstadoId { get; set; }

        public int? BeneficioPlanExequialId { get; set; }
    }
}