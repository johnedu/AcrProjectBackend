using Bow.Cartera.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class PlanExequial : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool PlanParaGrupo { get; set; }
        public bool PlanFamiliar { get; set; }
        public bool PlanEmpresarial { get; set; }
        public int MonedaId { get; set; }
        public virtual Moneda MonedaPlanExequial { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado EstadoPlanExequial { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public int cantidadDiasMora { get; set; }

        public virtual ICollection<GrupoFamiliar> GrupoFamiliarPlanExequial { get; set; }
        public virtual ICollection<BeneficioPlanExequial> BeneficioPlanExequial { get; set; }
        public virtual ICollection<BeneficioAdicionalPlanExequial> BeneficiosAdicionalesPlanExequial { get; set; }
        public virtual ICollection<PlanExequialSucursal> PlanExequialSucursales { get; set; }
        public virtual ICollection<PlanExequialRecaudoMasivo> PlanExequialRecaudosMasivos { get; set; }

        public PlanExequial()
        {
            GrupoFamiliarPlanExequial = new List<GrupoFamiliar>();
            BeneficioPlanExequial = new List<BeneficioPlanExequial>();
            BeneficiosAdicionalesPlanExequial = new List<BeneficioAdicionalPlanExequial>();
            PlanExequialSucursales = new List<PlanExequialSucursal>();
            PlanExequialRecaudosMasivos = new List<PlanExequialRecaudoMasivo>();
        }
    }
}