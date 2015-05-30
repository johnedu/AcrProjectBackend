using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class BeneficioOutPut : EntityDto, IOutputDto
    {
        public int TipoId { get; set; }
        public string TipoNombre { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
