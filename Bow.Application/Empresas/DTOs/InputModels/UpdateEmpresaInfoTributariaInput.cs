using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class UpdateEmpresaInfoTributariaInput : IInputDto
    {
        public int EmpresaId { get; set; }
        public List<EmpresaInfoTributariaInput> InfoTributarias { get; set; }
    }
}