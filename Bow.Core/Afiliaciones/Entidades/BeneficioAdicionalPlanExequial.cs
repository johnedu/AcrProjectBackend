using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class BeneficioAdicionalPlanExequial : EntidadMultiTenant
    {
        public int PlanExequialId { get; set; }
        public virtual PlanExequial PlanExequialBeneficioAdicionalPlanExequial { get; set; }
        public int BeneficioId { get; set; }
        public virtual Beneficio BeneficioBeneficioAdicionalPlanExequial { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int Asignables { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public int Valor { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado EstadoBeneficioAdicionalPlanExequial { get; set; }
        public int? BeneficioPlanExequialId { get; set; }
        public virtual BeneficioPlanExequial BeneficioPlanExequialAdicional { get; set; }

        public virtual ICollection<BeneficiosGestionProspecto> BeneficiosAdicionalesGestionProspecto { get; set; }

        public BeneficioAdicionalPlanExequial()
        {
            BeneficiosAdicionalesGestionProspecto = new List<BeneficiosGestionProspecto>();
        }
    }
}