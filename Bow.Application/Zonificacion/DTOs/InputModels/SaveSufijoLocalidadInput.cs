using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SaveSufijoLocalidadInput : IInputDto
    {
        public int localidadId { get; set; }
        public int sufijoId { get; set; }
    }
}
