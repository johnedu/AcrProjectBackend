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
    public class MensajeMap : MultiTenantMap<Mensaje>
    {
        public MensajeMap()
        {
            //Atributos
            Property(d => d.Titulo).HasMaxLength(512);
            Property(d => d.Titulo).IsRequired();

            Property(d => d.Contenido).HasMaxLength(4096);
            Property(d => d.Contenido).IsRequired();

            Property(d => d.FueLeido).IsRequired();

            //Tabla
            ToTable("mensaje");
        }
    }
}
