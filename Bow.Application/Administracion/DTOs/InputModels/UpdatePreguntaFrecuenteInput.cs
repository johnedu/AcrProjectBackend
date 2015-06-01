using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class UpdatePreguntaFrecuenteInput : EntityDto, IInputDto
    {
        [Required]
        [MaxLength(4096)]
        public string Pregunta { get; set; }
        [Required]
        [MaxLength(4096)]
        public string Respuesta { get; set; }
        public bool EstadoActiva { get; set; }
        public int Usuario { get; set; }
        public string Fecha { get; set; }
    }
}
