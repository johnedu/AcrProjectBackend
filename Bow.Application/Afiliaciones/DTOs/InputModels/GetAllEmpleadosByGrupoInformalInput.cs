using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class GetAllEmpleadosByGrupoInformalInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "afiliaciones_grupoInformalEmpleado_getall_grupoId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int GrupoInformalId { get; set; }
    }
}