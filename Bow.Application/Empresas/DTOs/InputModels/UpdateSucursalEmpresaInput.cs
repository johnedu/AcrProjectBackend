using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class UpdateSucursalEmpresaInput : EntityDto, IInputDto
    {
        public int EmpresaOrganizacionId { get; set; }
        public string Nombre { get; set; }
        public int TipoId { get; set; }
        public int DireccionId { get; set; }
        public int EstadoId { get; set; }
    }
}
