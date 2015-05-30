using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class GetAllSucursalesPlanExequialInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "sucursalesPlanExequial_getall_planExequialId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PlanExequialId { get; set; }

        [Required(ErrorMessageResourceName = "sucursalesPlanExequial_getall_empresaOrganizacionId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int EmpresaOrganizacionId { get; set; }
    }
}