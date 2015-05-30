using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class ValidarTelefonoOutput : IInputDto
    {
        public bool TelefonoValido { get; set; }
        public string MensajeValidacion { get; set; }
    }
}
