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
    public class JuegoMap : MultiTenantMap<Juego>
    {
        public JuegoMap()
        {
            //Atributos
            Property(d => d.Nombre).HasMaxLength(200);
            Property(d => d.Nombre).IsRequired();

            //Llaves Foráneas
            HasMany<Pregunta>(p => p.PreguntasJuego)
              .WithRequired(j => j.JuegoPregunta)
              .HasForeignKey(j => j.JuegoId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("juego");
        }
    }
}
