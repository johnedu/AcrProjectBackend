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
    public class TipoMap : MultiTenantMap<Tipo>
    {
        public TipoMap()
        {
            //Atributos
            Property(d => d.Nombre).HasMaxLength(512);
            Property(d => d.Nombre).IsRequired();

            //Llaves Foráneas
            HasMany<Usuario>(t => t.UsuariosTipo)
              .WithRequired(u => u.TipoUsuario)
              .HasForeignKey(u => u.TipoId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("tipo");
        }
    }
}
