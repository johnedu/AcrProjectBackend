using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class GetAllRangosParentescoByGrupoAndParentescoInput : EntityDto, IInputDto
    {
        [Required(ErrorMessageResourceName = "rangosParentescos_getall_planExequialId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int ParentescoId { get; set; }
        [Required(ErrorMessageResourceName = "rangosParentescos_getall_grupoFamiliarId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int GrupoFamiliarId { get; set; }
    }
}