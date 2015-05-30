using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class GetZonasByLocalidadAndTipoZonaInput : IInputDto
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
    }
}
