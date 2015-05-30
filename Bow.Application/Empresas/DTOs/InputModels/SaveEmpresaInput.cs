using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.InputModels
{
    public class SaveEmpresaInput : IInputDto
    {
        public int TipoNaturalezaId { get; set; }
        public int TipoDocumentoId { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public int? PersonaId { get; set; }
        public int ActividadEconomicaId { get; set; }
        public int DireccionId { get; set; }
    }
}