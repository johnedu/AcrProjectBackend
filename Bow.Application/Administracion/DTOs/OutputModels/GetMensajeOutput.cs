using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class GetMensajeOutput : EntityDto, IOutputDto
    {
        public string Emisor { get; set; }
        public int ReceptorId { get; set; }
        public string Receptor { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public bool FueLeido { get; set; }
    }
}
