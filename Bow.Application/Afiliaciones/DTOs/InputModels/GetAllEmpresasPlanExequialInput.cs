using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class GetAllEmpresasPlanExequialInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "empresasPlanExequial_getall_planExequialId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PlanExequialId { get; set; }

        [Required(ErrorMessageResourceName = "empresasPlanExequial_getall_organizacionId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int OrganizacionId { get; set; }
    }
}