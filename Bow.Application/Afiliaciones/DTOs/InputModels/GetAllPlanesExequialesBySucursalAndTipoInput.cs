using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class GetAllPlanesExequialesBySucursalAndTipoInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "bowAfiliacionPlanExequial_sucursalId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int SucursalId { get; set; }
        [Required(ErrorMessageResourceName = "bowAfiliacionPlanExequial_tipoPlan_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string TipoPlan { get; set; }
        public int? EmpresaOrGrupoId { get; set; }
        public int? RecaudoMasivoId { get; set; }
    }
}