using Bow.MappingsBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Mappings
{
    public class ZonaEmpleadoMap : MultiTenantMap<ZonaEmpleado>
    {
        public ZonaEmpleadoMap()
        {
            //Atributos
            Property(zonaEmpleado => zonaEmpleado.FechaAsignacion).IsOptional();
            Property(zonaEmpleado => zonaEmpleado.FechaRetiro).IsOptional();

            //Tabla
            ToTable("zona_empleado");
        }
    }
}