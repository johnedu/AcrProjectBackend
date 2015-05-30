using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SaveDireccionConNomenclaturaInput : EntityDto, IInputDto
    {
        public string Nombre { get; set; }
        public string Pista { get; set; }
        public int? ManzanaId { get; set; }

        [Required(ErrorMessageResourceName = "zonificacion_savedireccionconnomenclatura_barrio_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int? BarrioId { get; set; }

        [Required(ErrorMessageResourceName = "zonificacion_savedireccionconnomenclatura_viaprincipal_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int? TorieLocalidad1Id { get; set; }

        public int? Orientacion1 { get; set; }
        public int? SufijoLocalidad1Id { get; set; }

        [Required(ErrorMessageResourceName = "zonificacion_savedireccionconnomenclatura_viasecundaria_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int? TorieLocalidad2Id { get; set; }

        public int? Orientacion2 { get; set; }
        public int? SufijoLocalidad2Id { get; set; }

        [Required(ErrorMessageResourceName = "zonificacion_savedireccionconnomenclatura_porton_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(20, ErrorMessageResourceName = "zonificacion_savedireccionconnomenclatura_porton_max_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Porton { get; set; }

        public string Apartamento { get; set; }
        public string DireccionCompleta { get; set; }
        public string ZipCode { get; set; }
        public int LocalidadId { get; set; }
    }
}