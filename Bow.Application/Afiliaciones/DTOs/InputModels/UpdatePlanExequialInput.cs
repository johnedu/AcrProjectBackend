using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class UpdatePlanExequialInput : EntityDto, IInputDto
    {
        [Required(ErrorMessageResourceName = "planExequial_save_id_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "planExequial_save_nombre_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(150, ErrorMessageResourceName = "planExequial_save_nombre_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Nombre { get; set; }

        [Required(ErrorMessageResourceName = "planExequial_save_descripcion_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(300, ErrorMessageResourceName = "planExequial_save_descripcion_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Descripcion { get; set; }

        [Required(ErrorMessageResourceName = "planExequial_save_planGrupo_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public bool PlanParaGrupo { get; set; }

        [Required(ErrorMessageResourceName = "planExequial_save_planFamiliar_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public bool PlanFamiliar { get; set; }

        [Required(ErrorMessageResourceName = "planExequial_save_planEmpresarial_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public bool PlanEmpresarial { get; set; }

        [Required(ErrorMessageResourceName = "planExequial_save_estadoId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int EstadoId { get; set; }

        [Required(ErrorMessageResourceName = "planExequial_save_monedaId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int MonedaId { get; set; }

        public string FechaIngreso { get; set; }
        public string FechaCancelacion { get; set; }
    }
}
