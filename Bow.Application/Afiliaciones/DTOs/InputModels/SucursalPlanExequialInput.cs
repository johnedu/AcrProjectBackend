using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class SucursalPlanExequialInput : EntityDto, IInputDto
    {
        [Required(ErrorMessageResourceName = "sucursalPlanExequial_update_planExequialId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PlanExequialId { get; set; }

        [Required(ErrorMessageResourceName = "sucursalPlanExequial_update_sucursald_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int SucursalId { get; set; }

        [Required(ErrorMessageResourceName = "sucursalPlanExequial_update_asignadoAlPlan_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public bool AsignadoAlPlan { get; set; }
    }
}