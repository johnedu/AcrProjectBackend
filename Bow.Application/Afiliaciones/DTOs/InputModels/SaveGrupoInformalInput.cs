using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class SaveGrupoInformalInput : EntityDto
    {
        public string Nombre { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaCancelacion { get; set; }
        public int PorcentajeDescuento { get; set; }
        public bool EncargadoExento { get; set; }
        public int PersonaId { get; set; }
        public int EstadoId { get; set; }
        public int SucursalId { get; set; }
    }
}