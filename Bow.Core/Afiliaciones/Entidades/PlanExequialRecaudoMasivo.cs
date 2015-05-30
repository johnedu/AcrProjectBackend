using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class PlanExequialRecaudoMasivo : EntidadMultiTenant
    {
        public int PlanExequialId { get; set; }
        public virtual PlanExequial PlanExequialPlanExequialRecaudoMasivo { get; set; }
        public int RecaudoMasivoId { get; set; }
        public virtual RecaudoMasivo RecaudoMasivoPlanExequialRecaudoMasivo { get; set; }
        public bool EsObligatorio { get; set; }
    }
}