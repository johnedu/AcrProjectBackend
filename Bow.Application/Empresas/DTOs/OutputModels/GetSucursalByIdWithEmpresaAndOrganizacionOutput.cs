using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class GetSucursalByIdWithEmpresaAndOrganizacionOutput : IOutputDto 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreOrganizacion { get; set; }
    }
}
