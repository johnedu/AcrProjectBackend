using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class DetalleClienteProspectoOutput : EntityDto, IOutputDto
    {
        public int PlanExequialId { get; set; }
        public string PlanExequialNombre { get; set; }
        public bool PlanExequialEnSucursal { get; set; }

        public List<AfiliadoProspectoOutput> Afiliados { get; set; }
    }
}
