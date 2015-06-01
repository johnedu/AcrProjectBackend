using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class EnviarMensajeInput : IInputDto
    {
        public int UsuarioEmisorId { get; set; }
        [Required]
        public string CodaEmisor { get; set; }
        public int UsuarioReceptorId { get; set; }
        [Required]
        public string CodaReceptor { get; set; }
        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Contenido { get; set; }
        [Required]
        public bool FueLeido { get; set; }
    }
}
