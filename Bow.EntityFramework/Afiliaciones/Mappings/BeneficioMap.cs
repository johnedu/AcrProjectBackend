using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class BeneficioMap : MultiTenantMap<Beneficio>
    {
        public BeneficioMap()
        {
            //Atributos
            Property(beneficio => beneficio.Nombre).IsRequired();

            //Llaves Foraneas

            HasMany<BeneficioPlanExequial>(b => b.BeneficioPlanExequial)
              .WithRequired(b => b.BeneficioBeneficioPlanExequial)
              .HasForeignKey(b => b.BeneficioId)
              .WillCascadeOnDelete(false);

            HasMany<BeneficioAdicionalPlanExequial>(b => b.BeneficiosAdicionalesPlanExequial)
              .WithRequired(b => b.BeneficioBeneficioAdicionalPlanExequial)
              .HasForeignKey(b => b.BeneficioId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("beneficio");
        }
    }
}