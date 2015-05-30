using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class DeleteGrupoFamiliarInput
    {
        [Required(ErrorMessageResourceName = "grupoFamiliar_delete_id_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int Id { get; set; }
    }
}
