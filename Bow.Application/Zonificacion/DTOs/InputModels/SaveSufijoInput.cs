using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SaveSufijoInput : IInputDto
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}
