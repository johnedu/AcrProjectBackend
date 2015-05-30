using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class GetSufijoLocalidadByLocalidadOutput : EntityDto, IOutputDto
    {
        public string NombreSufijo { get; set; }
    }
}