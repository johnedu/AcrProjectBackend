using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.OutputModels
{
    public class GetTipoOutput : EntityDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int ParametroId { get; set; }
    }
}