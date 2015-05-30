using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class GetAllGrupoInformalByContactoInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "afiliaciones_grupoInformal_getall_gruposByContacto_personaId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PersonaId { get; set; }
    }
}