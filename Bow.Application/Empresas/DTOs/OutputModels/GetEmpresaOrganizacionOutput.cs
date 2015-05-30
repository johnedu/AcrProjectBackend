using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class GetEmpresaOrganizacionOutput : EntityDto
    {
        public string Nombre { get; set; }
        public int TipoNaturalezaId { get; set; }
        public int PaisTipoDocumentoId { get; set; }
        public string PaisTipoDocumentoNombre { get; set; }
        public int TipoDocumentoId { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public int? PersonaId { get; set; }
        public int ActividadEconomicaId { get; set; }
        public string ActividadEconomicaCodigo { get; set; }
        public string ActividadEconomicaNombre { get; set; }
        public int DireccionId { get; set; }
        public int LocalidadId { get; set; }
        public string Direccion { get; set; }
        public int EstadoId { get; set; }
        public string Estado { get; set; }
    }
}
