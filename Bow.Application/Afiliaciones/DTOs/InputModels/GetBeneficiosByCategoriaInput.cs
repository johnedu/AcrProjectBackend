using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class GetBeneficiosByCategoriaInput : EntityDto
    {
        public int TipoCategoriaId { get; set; }
        public int PlanExequialId { get; set; }
        public string GrupoBeneficio { get; set; }
    }
}
