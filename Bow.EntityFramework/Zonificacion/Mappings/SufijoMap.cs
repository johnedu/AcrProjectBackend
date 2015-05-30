using Bow.MappingsBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Mappings
{
    public class SufijoMap : MultiTenantMap<Sufijo>
    {
        public SufijoMap()
        {
            //Atributos
            Property(sufijo => sufijo.Nombre).HasMaxLength(50);
            Property(sufijo => sufijo.Nombre).IsRequired();

            //Llaves Foraneas
            HasMany<SufijoLocalidad>(sufijo => sufijo.SufijosLocalidades)
              .WithRequired(sufijoLocalidad => sufijoLocalidad.SufijoSufijoLocalidad)
              .HasForeignKey(sufijoLocalidad => sufijoLocalidad.SufijoId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("sufijo");
        }
    }
}
