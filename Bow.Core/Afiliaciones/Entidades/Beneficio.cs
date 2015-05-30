using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class Beneficio : EntidadMultiTenant
    {
        public int TipoId { get; set; }
        public Tipo TipoBeneficio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<BeneficioPlanExequial> BeneficioPlanExequial { get; set; }
        public virtual ICollection<BeneficioAdicionalPlanExequial> BeneficiosAdicionalesPlanExequial { get; set; }

        public Beneficio()
        {
            BeneficioPlanExequial = new List<BeneficioPlanExequial>();
            BeneficiosAdicionalesPlanExequial = new List<BeneficioAdicionalPlanExequial>();
        }
    }
}
