using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class ValidarDocumentoExisteInput : EntityDto
    {
        public string NumeroDocumento { get; set; }
        public int TipoDocumentoId { get; set; }
    }
}
