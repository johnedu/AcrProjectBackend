using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class GetLocalidadWithDepartamentoAndPaisOutput : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
        public string Indicativo { get; set; }
        public int DepartamentoId { get; set; }
        public string DepartamentoNombre { get; set; }
        public string DepartamentoIndicativo { get; set; }
        public int PaisId { get; set; }
        public string PaisNombre { get; set; }
        public string PaisIndicativo { get; set; }
        
    }
}
