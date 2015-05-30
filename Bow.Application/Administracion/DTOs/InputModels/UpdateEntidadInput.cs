using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class UpdateEntidadInput : EntityDto, IInputDto
    {
        [Required]
        [MaxLength(4096)]
        public string Texto { get; set; }
        [Required]
        public bool Comodin50_50 { get; set; }
        [Required]
        public bool RespuestaVerdadera { get; set; }
        public int PreguntaId { get; set; }
        public bool EstadoActiva { get; set; }
        public int Usuario { get; set; }
        public string Fecha { get; set; }
    }
}
