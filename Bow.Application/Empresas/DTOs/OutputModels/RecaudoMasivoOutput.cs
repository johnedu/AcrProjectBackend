using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class RecaudoMasivoOutput : EntityDto 
    {
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int NumeroLocalidades { get; set; }
    }
}
