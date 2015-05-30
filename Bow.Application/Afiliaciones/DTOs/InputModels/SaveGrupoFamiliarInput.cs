using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class SaveGrupoFamiliarInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "grupoFamiliar_save_nombre_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(50, ErrorMessageResourceName = "grupoFamiliar_save_nombre_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Nombre { get; set; }
        
        [Required(ErrorMessageResourceName = "grupoFamiliar_save_descripcion_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(200, ErrorMessageResourceName = "grupoFamiliar_save_descripcion_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Descripcion { get; set; }
        
        public int? CantidadMaximaAfiliados { get; set; }
        
        [Required(ErrorMessageResourceName = "grupoFamiliar_save_permitirAfiliadosAdicionales_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(1, ErrorMessageResourceName = "grupoFamiliar_save_permitirAfiliadosAdicionales_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string PermitirAfiliadosAdicionales { get; set; }
        
        [Required(ErrorMessageResourceName = "grupoFamiliar_save_valorBase_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int ValorPlan { get; set; }
        
        [Required(ErrorMessageResourceName = "grupoFamiliar_save_tieneCuotaInicial_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(1, ErrorMessageResourceName = "grupoFamiliar_save_tieneCuotaInicial_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string TieneCuotaInicial { get; set; }
        
        public int? ValorCuotaInicial { get; set; }
        
        [Required(ErrorMessageResourceName = "grupoFamiliar_save_planExequialId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PlanExequialId { get; set; }
        
        [Required(ErrorMessageResourceName = "grupoFamiliar_save_estadoId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int EstadoId { get; set; }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Validaciones Personalizadas
        /// </summary>
        /// <param name="results"></param>
        /// ///////////////////////////////////////////////////////////////////
        public void AddValidationErrors(List<ValidationResult> results)
        {
            //  Indicó que tiene cuota inicial, se valida que tenga dicho valor
            if ((TieneCuotaInicial == BowConsts.GRUPOFAMILIAR_VALOR_VERDADERO) && (!ValorCuotaInicial.HasValue))
            {
                results.Add(new ValidationResult(Bow.Resources.TextosValidacionDTO.grupoFamiliar_save_valorInicial_requerido));
            }
        }
    }
}