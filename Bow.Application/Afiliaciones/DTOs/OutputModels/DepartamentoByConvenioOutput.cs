using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class DepartamentoByConvenioOutput : EntityDto 
    {
        public string Nombre { get; set; }
        public int PaisId { get; set; }
        public string Pais { get; set; }
    }
}
