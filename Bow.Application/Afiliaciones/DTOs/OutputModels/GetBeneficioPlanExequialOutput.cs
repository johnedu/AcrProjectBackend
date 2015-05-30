using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetBeneficioPlanExequialOutput : EntityDto, IOutputDto
    {
        public string NombreBeneficio { get; set; }
        public string DescripcionBeneficio { get; set; }
        public string CategoriaBeneficioId { get; set; }
        public string CategoriaBeneficioNombre { get; set; }
        public bool EsAsignable { get; set; }
        public int Asignables { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaCancelacion { get; set; }
        public int EstadoId { get; set; }
        public int PlanExequialId { get; set; }
        public int BeneficioId { get; set; }
    }
}
