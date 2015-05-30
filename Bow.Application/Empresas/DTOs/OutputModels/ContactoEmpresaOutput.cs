using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class ContactoEmpresaOutput : EntityDto 
    {
        public string DocumentoPersona { get; set; }
        public int IdPersona { get; set; }
        public string NombrePersona { get; set; }
        public string TelefonosContacto { get; set; }
        public int IdTipoAreaEmpresa { get; set; }
        public string TipoAreaEmpresa { get; set; }
        public string Cargo { get; set; }
        public string Accion { get; set; }
    }
}
