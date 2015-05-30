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
    public class AvenidaMap : MultiTenantMap<Avenida>
    {
        public AvenidaMap()
        {
            //Atributos
            Property(avenida => avenida.Nombre).HasMaxLength(50);
            Property(avenida => avenida.Nombre).IsRequired();

            //Tabla
            ToTable("avenida");
        }
    }
}
