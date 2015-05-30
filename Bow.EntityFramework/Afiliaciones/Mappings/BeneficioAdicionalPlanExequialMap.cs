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
    public class BeneficioAdicionalPlanExequialMap: MultiTenantMap<BeneficioAdicionalPlanExequial>
    {
        public BeneficioAdicionalPlanExequialMap()
        {
            //Atributos
            Property(plan => plan.FechaIngreso).IsRequired();

            Property(plan => plan.Asignables).IsRequired();

            Property(plan => plan.Valor).IsRequired();

            //Llaves Foraneas
            HasMany<BeneficiosGestionProspecto>(paren => paren.BeneficiosAdicionalesGestionProspecto)
           .WithRequired(beneficiosgestionprospecto => beneficiosgestionprospecto.BeneficioAdicionalPlanExequial)
           .HasForeignKey(beneficiosgestionprospecto => beneficiosgestionprospecto.BeneficioAdicionalPlanExequialId)
           .WillCascadeOnDelete(false);

            //Tabla
            ToTable("beneficio_adicional_plan_exequial");
        }
    }
}