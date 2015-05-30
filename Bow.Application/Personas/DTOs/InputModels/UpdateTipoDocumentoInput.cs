using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.InputModels
{
    public class UpdateTipoDocumentoInput : EntityDto, IInputDto
    {
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        [Required]
        public int LongitudMinima { get; set; }
        [Required]
        public int LongitudMaxima { get; set; }
        [Required]
        [MaxLength(1)]
        public string ConjuntoCaracteres { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
        public string Default { get; set; }
        public string AplicaEmpresa { get; set; }
        public string AplicaPersona { get; set; }
        public int PaisId { get; set; }
    }
}
