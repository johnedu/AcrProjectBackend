using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class CambiarEstadoPlanExequialInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "planExequial_cambiarEstado_id_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "planExequial_cambiarEstado_estado_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public bool Estado { get; set; }
    }
}
