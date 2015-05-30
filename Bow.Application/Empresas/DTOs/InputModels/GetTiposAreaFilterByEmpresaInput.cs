using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class GetTiposAreaFilterByEmpresaInput : EntityDto
    {
        public int EmpresaId { get; set; }
    }
}
