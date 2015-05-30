using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class UpdateZonaInput : EntityDto, IInputDto
    {
        [Required(ErrorMessage = "NombreRequerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}