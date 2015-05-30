using Bow.Afiliaciones.Entidades;
using Bow.Cartera.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class PlanExequialMap: MultiTenantMap<PlanExequial>
    {
        public PlanExequialMap()
        {
            //Atributos
            Property(plan => plan.Nombre).HasMaxLength(150);
            Property(plan => plan.Nombre).IsRequired();

            Property(plan => plan.Descripcion).HasMaxLength(300);
            Property(plan => plan.Descripcion).IsRequired();

            Property(plan => plan.PlanParaGrupo).IsRequired();
            Property(plan => plan.PlanFamiliar).IsRequired();
            Property(plan => plan.PlanEmpresarial).IsRequired();

            Property(plan => plan.FechaIngreso).IsRequired();

            //Llaves Foraneas
            HasMany<GrupoFamiliar>(grupo => grupo.GrupoFamiliarPlanExequial)
              .WithRequired(plan => plan.PlanExequialGrupoFamiliar)
              .HasForeignKey(plan => plan.PlanExequialId)
              .WillCascadeOnDelete(false);

            HasMany<BeneficioPlanExequial>(b => b.BeneficioPlanExequial)
              .WithRequired(plan => plan.PlanExequialBeneficioPlanExequial)
              .HasForeignKey(plan => plan.PlanExequialId)
              .WillCascadeOnDelete(false);

            HasMany<BeneficioAdicionalPlanExequial>(b => b.BeneficiosAdicionalesPlanExequial)
              .WithRequired(plan => plan.PlanExequialBeneficioAdicionalPlanExequial)
              .HasForeignKey(plan => plan.PlanExequialId)
              .WillCascadeOnDelete(false);

            HasMany<PlanExequialSucursal>(s => s.PlanExequialSucursales)
              .WithRequired(plan => plan.PlanExequialPlanExequialSucursal)
              .HasForeignKey(plan => plan.PlanExequialId)
              .WillCascadeOnDelete(false);

            HasMany<PlanExequialRecaudoMasivo>(r => r.PlanExequialRecaudosMasivos)
              .WithRequired(plan => plan.PlanExequialPlanExequialRecaudoMasivo)
              .HasForeignKey(plan => plan.PlanExequialId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("plan_exequial");
        }
    }
}