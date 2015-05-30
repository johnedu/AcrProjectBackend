using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class ValidarApellidosParentescoInput : EntityDto
    {
        public int GestionProspectoId { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

    }
}
