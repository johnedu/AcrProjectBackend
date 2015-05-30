using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class ContactoWebEmpresaOutput : EntityDto 
    {
        public int IdMedioContacto { get; set; }
        public string MedioContacto { get; set; }
        public string Identificador { get; set; }
        public string Accion { get; set; }
    }
}
