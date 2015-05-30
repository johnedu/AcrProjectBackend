using Bow.MappingsBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Mappings
{
    public class ZonaBarrioMap : MultiTenantMap<ZonaBarrio>
    {
        public ZonaBarrioMap()
        {
            //Tabla
            ToTable("zona_barrio");
        }
    }
}
