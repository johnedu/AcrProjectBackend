using Bow.MappingsBase;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Mappings
{
    public class DimensionMap : MultiTenantMap<Dimension>
    {
        public DimensionMap()
        {
            //Atributos
            Property(d => d.Nombre).HasMaxLength(200);
            Property(d => d.Nombre).IsRequired();

            //Llaves Foráneas
            HasMany<Pregunta>(p => p.PreguntasDimension)
              .WithRequired(d => d.DimensionPregunta)
              .HasForeignKey(d => d.DimensionId)
              .WillCascadeOnDelete(false);

            HasMany<Entidad>(e => e.EntidadesDimension)
              .WithRequired(d => d.DimensionPregunta)
              .HasForeignKey(d => d.DimensionId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("dimension");
        }
    }
}
