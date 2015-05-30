using Abp.Application.Services.Dto;
using Abp.Localization;
using Abp.Runtime.Validation;
using Bow.CustomDataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class GetPersonasInput : EntityDto, IInputDto, ICustomValidate
    {
        [Required(ErrorMessageResourceName = "persona_tiene_documento_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public bool TieneDocumento { get; set; }
        public int? TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessageResourceName = "persona_nombre_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(50, ErrorMessageResourceName = "persona_nombre_max_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Nombre { get; set; }

        [Required(ErrorMessageResourceName = "persona_apellido1_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(50, ErrorMessageResourceName = "persona_apellido1_max_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Apellido1 { get; set; }

        [StringLength(50, ErrorMessageResourceName = "persona_apellido2_max_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Apellido2 { get; set; }

        [Required(ErrorMessageResourceName = "persona_nacionalidad_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int? PaisId { get; set; }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Validaciones Personalizadas
        /// </summary>
        /// <param name="results"></param>
        /// ///////////////////////////////////////////////////////////////////
        public void AddValidationErrors(List<ValidationResult> results)
        {
            //Persona con documento y sin tipo de documento
            if ((TieneDocumento == true) && (!TipoDocumentoId.HasValue))
            {
                results.Add(new ValidationResult(Bow.Resources.TextosValidacionDTO.persona_tipo_documento_requerido));
            }
            //Persona con documento y sin número de documento
            else if ((TieneDocumento == true) && (NumeroDocumento == null))
            {
                results.Add(new ValidationResult(Bow.Resources.TextosValidacionDTO.persona_numero_documento_requerido));
            }
        }
    }
}
