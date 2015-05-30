using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class GetSucursalEmpresaOrganizacionOutput : EntityDto
    {
        public int EmpresaOrganizacionId { get; set; }
        public string Nombre { get; set; }
        public int TipoId { get; set; }
        public string TipoSucursal { get; set; }
        public int EstadoId { get; set; }
        public string EstadoSucursal { get; set; }
        public int DireccionId { get; set; }
        public int LocalidadId { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public List<TelefonoSucursalOutput> Telefonos { get; set; }
    }
}
