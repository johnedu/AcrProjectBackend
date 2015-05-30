using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetRangoParentescoOutput : EntityDto, IOutputDto
    {
        public int GrupoFamiliarParentescoId { get; set; }
        public int EdadMinima { get; set; }
        public int EdadMaxima { get; set; }
        public int PeriodoCarencia { get; set; }
        public string UnidadPeriodoCarencia { get; set; }
        public string TipoValorBasico { get; set; }
        public int ValorBasico { get; set; }
        public string TipoValorAdicional { get; set; }
        public int ValorAdicional { get; set; }
        public int ParentescoId { get; set; }
    }
}
