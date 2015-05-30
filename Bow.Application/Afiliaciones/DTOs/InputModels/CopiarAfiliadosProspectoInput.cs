using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class CopiarAfiliadosProspectoInput : IInputDto
    {
        public int GestionProspectoId { get; set; }
        public List<AfiliadoProspectoInput> Afiliados { get; set; }
    }
}
