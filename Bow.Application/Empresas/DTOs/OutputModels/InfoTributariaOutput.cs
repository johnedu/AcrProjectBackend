using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class InfoTributariaOutput : EntityDto 
    {
        public string Nombre { get; set; }
        public string TipoValor { get; set; }
        public int CantidadOpciones { get; set; }
        public int CantidadLocalidades { get; set; }
        public bool Estado { get; set; }
    }
}
