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
    public class PreguntaMap : MultiTenantMap<Pregunta>
    {
        public PreguntaMap()
        {
            //Atributos
            Property(d => d.Texto).HasMaxLength(4096);
            Property(d => d.Texto).IsRequired();

            Property(d => d.Pista).HasMaxLength(4096);
            Property(d => d.Pista).IsRequired();

            Property(d => d.Nivel).HasMaxLength(1);
            Property(d => d.Nivel).IsRequired();

            //Llaves Foráneas
            HasMany<Puntaje>(p => p.PuntajesPregunta)
              .WithRequired(p => p.PreguntaPuntaje)
              .HasForeignKey(p => p.PreguntaId)
              .WillCascadeOnDelete(false);

            HasMany<Respuesta>(p => p.RespuestasPregunta)
              .WithRequired(r => r.PreguntaRespuesta)
              .HasForeignKey(r => r.PreguntaId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("pregunta");
        }
    }
}
