using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GestionProspectoOutput : EntityDto, IOutputDto
    {
        public int ProspectoId { get; set; }
        public int EmpleadoId { get; set; }
        public int PersonaId { get; set; }
        public string PersonaNombre { get; set; }
        public int EstadoNoAfiliacionId { get; set; }
        public string EstadoNoAfiliacionMotivo { get; set; }
        public int FunerariaAfiliadoId { get; set; }
        public int GrupoFamiliarId { get; set; }
        public int SucursalId { get; set; }
        public int LocalidadId { get; set; }
        public DateTime FechaGestion { get; set; }
        public DateTime FechaBloqueo { get; set; }
        public string EmpresaAfiliada { get; set; }
        public string Observaciones { get; set; }

    }
}
