using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class OpcionesInfoTributariaEmpresaOutput : EntityDto
    {
        public int InfoTributariaId { get; set; }
        public int InfoTributariaOpcionId { get; set; }
        public string InfoTributaria { get; set; }
        public string InfoTributariaOpcion { get; set; }
        public string TipoValor { get; set; }
        public string Valor { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public bool EstadoActiva { get; set; }
        public string FechaActualizacion { get; set; }
        public string Accion { get; set; }
        public string IdEliminar { get; set; }
    }
}
