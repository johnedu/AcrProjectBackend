using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class GrupoFamiliar : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? CantidadMaximaAfiliados { get; set; }
        public string PermitirAfiliadosAdicionales { get; set; }
        public int ValorPlan { get; set; }
        public string TieneCuotaInicial { get; set; }
        public int? ValorCuotaInicial { get; set; }
        public int PlanExequialId { get; set; }
        public virtual PlanExequial PlanExequialGrupoFamiliar { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado EstadoGrupoFamiliar { get; set; }

        public virtual ICollection<GrupoFamiliarParentesco> GruposFamiliaresParentesco { get; set; }
        public virtual ICollection<GestionProspecto> GruposFamiliaresGestionProspecto { get; set; }

        public GrupoFamiliar()
        {
            GruposFamiliaresParentesco = new List<GrupoFamiliarParentesco>();
            GruposFamiliaresGestionProspecto = new List<GestionProspecto>();
        }
    }
}