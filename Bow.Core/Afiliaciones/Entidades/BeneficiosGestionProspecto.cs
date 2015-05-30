using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
   public class BeneficiosGestionProspecto: EntidadMultiTenant
    {
        public int GestionProspectoId { get; set; }
        public GestionProspecto GestionProspecto { get; set; }
        public int BeneficioAdicionalPlanExequialId { get; set; }
        public BeneficioAdicionalPlanExequial BeneficioAdicionalPlanExequial { get; set; }
    }
}

