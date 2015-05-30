using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados.DTOs.InputModels
{
    public class SaveEmpleadoInput : IInputDto
    {
        public int Codigo { get; set; }
        public int PersonaId { get; set; }
        public int SucursalId { get; set; }
        public int EstadoId { get; set; }
    }
}
