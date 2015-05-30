using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SaveDireccionSinNomenclaturaInput : EntityDto, IInputDto
    {
        [Required(ErrorMessageResourceName = "zonificacion_savedireccionsinnomenclatura_barrio_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int? BarrioId { get; set; }

        [Required(ErrorMessageResourceName = "zonificacion_savedireccionsinnomenclatura_nombre_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(150, ErrorMessageResourceName = "zonificacion_savedireccionsinnomenclatura_nombre_max_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Nombre { get; set; }

        [StringLength(300, ErrorMessageResourceName = "zonificacion_savedireccionsinnomenclatura_pista_max_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string Pista { get; set; }

        public string DireccionCompleta { get; set; }
    }
}
