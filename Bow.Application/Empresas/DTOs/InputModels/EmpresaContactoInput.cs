using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class EmpresaContactoInput : EntityDto
    {
        public int EmpresaId { get; set; }
        public int PersonaId { get; set; }
        public int TipoAreaEmpresaId { get; set; }
        public string Cargo { get; set; }
        public string Accion { get; set; }
    }
}