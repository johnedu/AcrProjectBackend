using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class SaveOrUpdateGrupoFamiliarParentescoInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "grupoFamiliarParentesco_save_parentescoId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int ParentescoId { get; set; }
        [Required(ErrorMessageResourceName = "grupoFamiliarParentesco_save_grupoFamiliarId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int GrupoFamiliarId { get; set; }
        [Required(ErrorMessageResourceName = "grupoFamiliarParentesco_save_validarSoloIngreso_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(50, ErrorMessageResourceName = "grupoFamiliarParentesco_save_validarSoloIngreso_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string ValidarSoloIngreso { get; set; }
    }
}