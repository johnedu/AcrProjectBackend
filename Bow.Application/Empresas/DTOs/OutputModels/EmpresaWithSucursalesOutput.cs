using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class EmpresaWithSucursalesOutput : EntityDto
    {
        public string Empresa { get; set; }
        public int NumeroSucursales { get; set; }
    }
}
