using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetGrupoInformalOutput : EntityDto
    {
        public string Nombre { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public int PorcentajeDescuento { get; set; }
        public bool EncargadoExento { get; set; }
        public int PersonaId { get; set; }
        public string Documento { get; set; }
        public string PersonaNombre { get; set; }
        public int EstadoId { get; set; }
        public int SucursalId { get; set; }
        public string SucursalNombre { get; set; }
        public string SucursalNombreEmpresa { get; set; }
        public string SucursalNombreOrganizacion { get; set; }
    }
}
