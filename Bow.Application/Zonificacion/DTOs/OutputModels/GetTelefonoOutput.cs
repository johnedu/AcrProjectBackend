using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
   public class GetTelefonoOutput : EntityDto
    {
        public string Numero { get; set; }
        public string LocalidadNombre { get; set; }
        public int LocalidadId { get; set; }
    }
}
