using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class SaveInfoTributariaInput : IInputDto
    {
        public string Nombre { get; set; }
        public int? TipoValorId { get; set; }
        public int EstadoId { get; set; }
    }
}