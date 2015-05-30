using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class SaveOpcionPreferenciaInput : IInputDto
    {
        [Required]
        [MaxLength(80)]
        public string Nombre { get; set; }
        [Required]
        public int PreferenciaId { get; set; }
    }
}
