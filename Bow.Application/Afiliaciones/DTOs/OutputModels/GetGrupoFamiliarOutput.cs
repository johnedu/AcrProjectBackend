using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetGrupoFamiliarOutput : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? CantidadMaximaAfiliados { get; set; }
        public string PermitirAfiliadosAdicionales { get; set; }
        public int ValorPlan { get; set; }
        public string TieneCuotaInicial { get; set; }
        public int ValorCuotaInicial { get; set; }
        public int PlanExequialId { get; set; }
        public int EstadoId { get; set; }
    }
}
