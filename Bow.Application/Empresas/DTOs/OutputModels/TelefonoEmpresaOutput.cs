using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class TelefonoEmpresaOutput : EntityDto
    {
        public int TelefonoId { get; set; }
        public string TelefonoNumero { get; set; }
        public string NombreLocalidad { get; set; }
        public string Accion { get; set; }
    }
}
