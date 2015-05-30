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
    public class BeneficioPlanExequialMap: MultiTenantMap<BeneficioPlanExequial>
    {
        public BeneficioPlanExequialMap()
        {
            //Atributos
            Property(plan => plan.FechaIngreso).IsRequired();

            Property(plan => plan.Asignables).IsRequired();

            //Laves foraneas
            HasMany<BeneficioAdicionalPlanExequial>(b => b.BeneficiosAdicionalesPlanExequial)
            .WithOptional(ba => ba.BeneficioPlanExequialAdicional)
            .HasForeignKey(ba => ba.BeneficioPlanExequialId)
            .WillCascadeOnDelete(false);

            //Tabla
            ToTable("beneficio_plan_exequial");
        }
    }
}