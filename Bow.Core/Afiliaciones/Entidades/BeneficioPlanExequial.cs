using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class BeneficioPlanExequial : EntidadMultiTenant
    {
        public int PlanExequialId { get; set; }
        public virtual PlanExequial PlanExequialBeneficioPlanExequial { get; set; }
        public int BeneficioId { get; set; }
        public virtual Beneficio BeneficioBeneficioPlanExequial { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int Asignables { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado EstadoBeneficioPlanExequial { get; set; }

        public virtual ICollection<BeneficioAdicionalPlanExequial> BeneficiosAdicionalesPlanExequial { get; set; }

        public BeneficioPlanExequial()
        {
            BeneficiosAdicionalesPlanExequial = new List<BeneficioAdicionalPlanExequial>();
        }

    }
}