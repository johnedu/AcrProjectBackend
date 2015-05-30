using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados.DTOs.InputModels
{
    public class GetEmpleadosByIdInput :  IInputDto
    {
        public int Id { get; set; }
    }
}
