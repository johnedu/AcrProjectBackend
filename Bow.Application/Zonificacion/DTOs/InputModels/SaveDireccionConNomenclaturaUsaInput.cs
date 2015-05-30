using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SaveDireccionConNomenclaturaUsaInput : EntityDto, IInputDto
    {
        public string Nombre { get; set; }
        public string Pista { get; set; }
        public int? ManzanaId { get; set; }
        public int? BarrioId { get; set; }
        public int? TorieLocalidad1Id { get; set; }

        [Required(ErrorMessageResourceName = "zonificacion_savedireccionconnomenclaturausa_numero_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(20, ErrorMessageResourceName = "zonificacion_savedireccionconnomenclaturausa_porton_max_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Porton { get; set; }

        [Required(ErrorMessageResourceName = "zonificacion_savedireccionconnomenclaturausa_nombrevia_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int? Orientacion1 { get; set; }

        [Required(ErrorMessageResourceName = "zonificacion_savedireccionconnomenclaturausa_tipovia_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int? SufijoLocalidad1Id { get; set; }

        [Required(ErrorMessageResourceName = "zonificacion_savedireccionconnomenclaturausa_zipcode_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string ZipCode { get; set; }

        public int? TorieLocalidad2Id { get; set; }
        public int? Orientacion2 { get; set; }
        public int? SufijoLocalidad2Id { get; set; }

        public string Apartamento { get; set; }
        public string DireccionCompleta { get; set; }

        public int LocalidadId { get; set; }
    }
}