using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class UpdateSucursalEmpresaTelefonoInput : IInputDto
    {
        public int SucursalId { get; set; }
        public List<SucursalTelefonoInput> Telefonos { get; set; }
    }
}