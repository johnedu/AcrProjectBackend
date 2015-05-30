using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class SavePersonaInput : EntityDto, IInputDto, ICustomValidate
    {
        public int Id { get; set; }

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

        [Required(ErrorMessageResourceName = "persona_tiene_documento_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public bool TieneDocumento { get; set; }

        public int? TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime? FechaExpDocumento { get; set; }

        [Required(ErrorMessageResourceName = "persona_tiene_fechanacimiento_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public bool TieneFechaNacimiento { get; set; }

        public bool FechaNacimientoRequerido { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public string Genero { get; set; }

        [StringLength(80, ErrorMessageResourceName = "persona_correo_electronico_max_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessageResourceName = "persona_correo_electronico_invalido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string CorreoElectronico { get; set; }

        public bool ContactarSms { get; set; }
        public bool ContactarCorreo { get; set; }
        public bool ContactarTelefono { get; set; }
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessageResourceName = "persona_profesion_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int? TipoProfesionId { get; set; }

        [Required(ErrorMessageResourceName = "persona_estadocivil_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int TipoEstadoCivilId { get; set; }

        public bool Discapacitada { get; set; }
        public DateTime? FechaFallecimiento { get; set; }

        public DateTime FechaUltActualizacion { get; set; }
        public string Usuario { get; set; }

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
            //Persona con fecha de nacimiento y sin valor de fecha de nacimiento
            else if ((TieneFechaNacimiento == true) && (FechaNacimiento == null))
            {
                results.Add(new ValidationResult(Bow.Resources.TextosValidacionDTO.persona_fecha_nacimiento_requerido));
            }
            else if ((FechaNacimientoRequerido == true) && (FechaNacimiento == null))
            {
                results.Add(new ValidationResult(Bow.Resources.TextosValidacionDTO.persona_fecha_nacimiento_requerido));
            }

        }

    }
}