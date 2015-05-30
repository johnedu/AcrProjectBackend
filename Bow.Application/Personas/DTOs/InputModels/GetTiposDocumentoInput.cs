using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class GetTiposDocumentoInput : EntityDto
    {
        public int PaisId { get; set; }
    }
}
