using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.InputModels
{
    public class UpdateTipoInput : EntityDto, IInputDto
    {
        [Required(ErrorMessage = "NombreRequerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        //public int ParametroId { get; set; }
    }
}
