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
    public class EntidadMap : MultiTenantMap<Entidad>
    {
        public EntidadMap()
        {
            //Atributos
            Property(d => d.Nombre).HasMaxLength(512);
            Property(d => d.Nombre).IsRequired();
            Property(d => d.Descripcion).HasMaxLength(4096);
            Property(d => d.Descripcion).IsRequired();

            //Tabla
            ToTable("entidad");
        }
    }
}
