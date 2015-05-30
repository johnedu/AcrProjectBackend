using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class UpdateEmpresaContactosInput : IInputDto
    {
        public int EmpresaId { get; set; }
        public List<EmpresaContactoInput> Contactos { get; set; }
    }
}