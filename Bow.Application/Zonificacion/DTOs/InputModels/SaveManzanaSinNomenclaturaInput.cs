using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SaveManzanaSinNomenclaturaInput : IInputDto
    {
        [Required]
        [MaxLength(60)]
        public string Nombre { get; set; }
        public int BarrioId { get; set; }
    }
}
