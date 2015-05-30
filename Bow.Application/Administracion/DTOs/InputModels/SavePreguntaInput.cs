using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class SavePreguntaInput : IInputDto
    {
        [Required]
        [MaxLength(4096)]
        public string Texto { get; set; }
        [Required]
        [MaxLength(1)]
        public string Nivel { get; set; }
        [Required]
        [MaxLength(4096)]
        public string Pista { get; set; }
        public int JuegoId { get; set; }
        public int DimensionId { get; set; }
        public bool EstadoActiva { get; set; }
        public int Usuario { get; set; }
        public string Fecha { get; set; }
    }
}
