using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class DeleteInfoTributariaLocalidadInput : IInputDto
    {
        public int InfoTributariaId { get; set; }
        public int LocalidadId { get; set; }
    }
}