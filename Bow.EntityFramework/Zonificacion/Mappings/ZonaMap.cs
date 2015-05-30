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
    public class ZonaMap : MultiTenantMap<Zona>
    {
        public ZonaMap()
        {
            //Atributos
            Property(zona => zona.Nombre).HasMaxLength(100);
            Property(zona => zona.Nombre).IsRequired();

            //Llaves Foraneas
            HasMany<ZonaBarrio>(zona => zona.ZonasBarrios)
              .WithRequired(ZonaBarrio => ZonaBarrio.ZonaZonaBarrio)
              .HasForeignKey(ZonaBarrio => ZonaBarrio.ZonaId);

            HasMany<ZonaEmpleado>(zona => zona.ZonasEmpleado)
              .WithRequired(ZonaEmpleado => ZonaEmpleado.ZonaZonaEmpleado)
              .HasForeignKey(ZonaEmpleado => ZonaEmpleado.ZonaId);

            //Tabla
            ToTable("zona");
        }

    }
}
