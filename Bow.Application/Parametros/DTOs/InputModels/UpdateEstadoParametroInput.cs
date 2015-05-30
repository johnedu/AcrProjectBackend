using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.InputModels
{
    public class UpdateEstadoParametroInput : EntityDto, IInputDto
    {
        [Required(ErrorMessage = "MotivoRequerido")]
        public string Motivo { get; set; }
    }
}