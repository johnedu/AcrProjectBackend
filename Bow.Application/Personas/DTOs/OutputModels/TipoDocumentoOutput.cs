using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class TipoDocumentoOutput : EntityDto
    {
        public string Nombre { get; set; }
        public int LongitudMinima { get; set; }
        public int LongitudMaxima { get; set; }
        public string ConjuntoCaracteres { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
        public string Default { get; set; }
        public string AplicaEmpresa { get; set; }
        public string AplicaPersona { get; set; }
    }
}
