using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class SaveInfoTributariaLocalidadnput : EntityDto
    {
        public int InfoTributariaId { get; set; }
        public int LocalidadId { get; set; }
    }
}