using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class EmpresaInfoTributariaInput : EntityDto
    {
        public int EmpresaId { get; set; }
        public int InfoTributariaOpcionId { get; set; }
        public int InfoTributariaId { get; set; }
        public string Valor { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string Accion { get; set; }
    }
}