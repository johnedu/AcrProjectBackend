using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class ValidarEdadParentescoInput: EntityDto
    {
        public string Operacion { get; set; }
        public int Edad1 { get; set; }
        public int Edad2 { get; set; }
    }
}
