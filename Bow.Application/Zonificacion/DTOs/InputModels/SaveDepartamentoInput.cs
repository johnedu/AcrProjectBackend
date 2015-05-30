using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SaveDepartamentoInput : IInputDto
    {
        public string Nombre { get; set; }
        public string Indicativo { get; set; }
        public int PaisId { get; set; }
    }
}
