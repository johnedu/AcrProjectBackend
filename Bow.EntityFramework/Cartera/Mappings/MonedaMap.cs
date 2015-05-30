using Bow.Afiliaciones.Entidades;
using Bow.Cartera.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Cartera.Mappings
{
    public class MonedaMap: MultiTenantMap<Moneda>
    {
        public MonedaMap()
        {
            //Atributos
            Property(empresa => empresa.Nombre).HasMaxLength(60);
            Property(empresa => empresa.Nombre).IsRequired();

            Property(empresa => empresa.Simbolo).HasMaxLength(10);
            Property(empresa => empresa.Simbolo).IsRequired();

            //Llaves Foraneas
            HasMany<PlanExequial>(monedaPlan => monedaPlan.PlanesExequiales)
              .WithRequired(planExequial => planExequial.MonedaPlanExequial)
              .HasForeignKey(planExequial => planExequial.MonedaId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("moneda");
        }
    }
}