using Abp.Application.Services.Dto;
using Bow.Parametros.DTOs.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.InputModels
{
    public class GetAllTiposContactoWebWithFilterPersonaInput : EntityDto
    {
        public List<TipoInput> Tipos { get; set; }
    }
}
