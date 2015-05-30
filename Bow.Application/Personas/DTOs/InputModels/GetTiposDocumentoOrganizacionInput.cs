using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class GetTiposDocumentoOrganizacionInput : EntityDto
    {
        public int PaisId { get; set; }
        public int NaturalezaEmpresa { get; set; }
    }
}
