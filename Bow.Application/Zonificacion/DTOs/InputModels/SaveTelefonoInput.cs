using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SaveTelefonoInput : IInputDto
    {
        [Required]
        [MaxLength(15)]
        public string Numero { get; set; }
        [MaxLength(5)]
        public string Extension { get; set; }
        public int TipoId { get; set; }
        public int LocalidadId { get; set; }
    }
}
