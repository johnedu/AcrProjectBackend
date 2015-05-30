using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class ParentescoAllRangosOutput : EntityDto, IOutputDto
    {
        public string NombreParentesco { get; set; }
        public string ValidarSoloIngreso { get; set; }

        public List<RangoParentescoOutput> Rangos { get; set; }
    }
}
