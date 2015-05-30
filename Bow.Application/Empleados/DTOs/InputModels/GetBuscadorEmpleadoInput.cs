using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados.DTOs.InputModels
{
    public class GetBuscadorEmpleadoInput : EntityDto
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Documento { get; set; }
        public string CorreoElectronico { get; set; }
        public int SucursalId { get; set; }
        public int EstadoId { get; set; }
    }
}