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
    public class UsuarioMap : MultiTenantMap<Usuario>
    {
        public UsuarioMap()
        {
            //Atributos
            Property(d => d.Nombre).HasMaxLength(512);
            Property(d => d.Nombre).IsRequired();

            Property(d => d.Coda).HasMaxLength(100);
            Property(d => d.Coda).IsRequired();

            //Llaves Foráneas
            HasMany<Mensaje>(u => u.MensajesEmisor)
              .WithRequired(m => m.UsuarioEmisor)
              .HasForeignKey(m => m.UsuarioEmisorId)
              .WillCascadeOnDelete(false);

            HasMany<Mensaje>(u => u.MensajesReceptor)
              .WithRequired(m => m.UsuarioReceptor)
              .HasForeignKey(m => m.UsuarioReceptorId)
              .WillCascadeOnDelete(false);

            HasMany<Puntaje>(u => u.PuntajesUsuario)
              .WithRequired(p => p.UsuarioPuntaje)
              .HasForeignKey(p => p.UsuarioId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("usuario");
        }
    }
}
