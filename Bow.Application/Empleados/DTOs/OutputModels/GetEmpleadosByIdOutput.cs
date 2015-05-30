using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados.DTOs.OutputModels
{
    public class GetEmpleadosByIdOutput : EntityDto
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string NombreCompleto { get; set; }
        public string SucursalId { get; set; }
        public string SucursalNombre { get; set; }
        public string EmpresaNombre { get; set; }
        public string OrganizacionNombre { get; set; }
        public int LocalidadId { get; set; }
        public string Localidad { get; set; }
        public string DepartamentoIndicativo { get; set; }
        public int DepartamentoId { get; set; }
        public string Departamento { get; set; }
        public string PaisIndicativo { get; set; }
        public int PaisId { get; set; }
        public string Pais { get; set; }
    }
}
