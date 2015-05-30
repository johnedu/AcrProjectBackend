using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class SaveRangoParentescoInput : IInputDto
    {
        [Required(ErrorMessageResourceName = "rangoParentesco_save_grupoParentescoId_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int GrupoFamiliarParentescoId { get; set; }

        [Required(ErrorMessageResourceName = "rangoParentesco_save_edadMinima_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int EdadMinima { get; set; }

        [Required(ErrorMessageResourceName = "rangoParentesco_save_edadMaxima_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int EdadMaxima { get; set; }

        [Required(ErrorMessageResourceName = "rangoParentesco_save_periodoCarencia_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int PeriodoCarencia { get; set; }

        [Required(ErrorMessageResourceName = "rangoParentesco_save_unidadPeriodoCarencia_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(1, ErrorMessageResourceName = "rangoParentesco_save_unidadPeriodoCarencia_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string UnidadPeriodoCarencia { get; set; }

        [Required(ErrorMessageResourceName = "rangoParentesco_save_tipoValorBasico_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(1, ErrorMessageResourceName = "rangoParentesco_save_tipoValorBasico_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string TipoValorBasico { get; set; }

        [Required(ErrorMessageResourceName = "rangoParentesco_save_valorBasico_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int ValorBasico { get; set; }

        [Required(ErrorMessageResourceName = "rangoParentesco_save_tipoValorAdicional_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        [StringLength(150, ErrorMessageResourceName = "rangoParentesco_save_tipoValorAdicional_longitud", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public string TipoValorAdicional { get; set; }

        [Required(ErrorMessageResourceName = "rangoParentesco_save_valorAdicional_requerido", ErrorMessageResourceType = typeof(Bow.Resources.TextosValidacionDTO))]
        public int ValorAdicional { get; set; }
    }
}