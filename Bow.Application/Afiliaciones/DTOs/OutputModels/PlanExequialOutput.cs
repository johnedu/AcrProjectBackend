using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class PlanExequialOutput : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool PlanParaGrupo { get; set; }
        public bool PlanFamiliar { get; set; }
        public bool PlanEmpresarial { get; set; }
        public int EstadoId { get; set; }
        public string Estado { get; set; }
        public bool EstadoActivo { get; set; }
        public int MonedaId { get; set; }
        public string Moneda { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaCancelacion { get; set; }
    }
}
