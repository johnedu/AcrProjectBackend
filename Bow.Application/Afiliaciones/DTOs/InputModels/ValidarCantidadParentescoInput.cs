using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class ValidarCantidadParentescoInput : EntityDto
    {
        public int GestionProspectoId { get; set; }
        public int AfiliadoProspectoId { get; set; }
        public int Repeticiones { get; set; }

    }
}
