using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.InputModels
{
    public class SaveNombreEstadoInput : IInputDto
    {
        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(2)]
        public string Abreviacion { get; set; }
    }
}
