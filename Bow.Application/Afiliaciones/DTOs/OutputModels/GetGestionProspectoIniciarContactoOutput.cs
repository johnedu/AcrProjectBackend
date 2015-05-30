using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetGestionProspectoIniciarContactoOutput : EntityDto, IOutputDto
    {
        public int ProspectoId { get; set; }
        public int EmpleadoId { get; set; }
        public int PersonaId { get; set; }
        public int EstadoNoAfiliacionId { get; set; }
        public int FunerariaAfiliadoId { get; set; }
        public string FunerariaAfiliadoNombre { get; set; }
        public int GrupoFamiliarId { get; set; }
        public int SucursalId { get; set; }
        public int LocalidadId { get; set; }
        public DateTime FechaGestion { get; set; }
        public DateTime FechaBloqueo { get; set; }
        public string EmpresaAfiliada { get; set; }
        public string Observaciones { get; set; }

        public string GrupoFamiliarNombre { get; set; }
        public int PlanExequialId { get; set; }
        public string PlanExequialNombre { get; set; }

        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string PaisId { get; set; }
        public string FechaNacimiento { get; set; }
        public bool PlanExequialEnSucursal { get; set; }

        public string EstadoNoAfiliacionMotivo { get; set; }

        public List<AfiliadoProspectoOutput> Afiliados { get; set; }
    }
}
