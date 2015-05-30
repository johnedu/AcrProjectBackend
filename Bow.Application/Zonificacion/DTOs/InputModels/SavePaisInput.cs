using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SavePaisInput : IInputDto
    {
        [Required]
        [MaxLength(25)]
        public string Nombre { get; set; }
        [Required]
        public string Indicativo { get; set; }
    }
}
