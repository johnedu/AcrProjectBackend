using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class EmpresaContactoWebInput : EntityDto
    {
        public int EmpresaId { get; set; }
        public int TipoRedId { get; set; }
        public string Identificador { get; set; }
        public string Accion { get; set; }
    }
}