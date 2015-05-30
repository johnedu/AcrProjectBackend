using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.InputModels
{
    public class SaveEstadoInput : IInputDto
    {
        public string Motivo { get; set; }
        public int EstadoNombreId { get; set; }
        public int ParametroId { get; set; }
    }
}
