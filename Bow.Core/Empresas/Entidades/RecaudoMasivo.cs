using Bow.Afiliaciones.Entidades;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Entidades
{
    public class RecaudoMasivo : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int OrganizacionId { get; set; }
        public virtual Organizacion OrganizacionRecaudoMasivo { get; set; }

        public virtual ICollection<RecaudoMasivoCobertura> RecaudoMasivoCobertura { get; set; }
        public virtual ICollection<PlanExequialRecaudoMasivo> PlanExequialRecaudosMasivos { get; set; }

        public RecaudoMasivo()
        {
            RecaudoMasivoCobertura = new List<RecaudoMasivoCobertura>();
            PlanExequialRecaudosMasivos = new List<PlanExequialRecaudoMasivo>();
        }
    }
}