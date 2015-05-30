using Bow.Afiliaciones.Entidades;
using Bow.Cartera.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class GrupoInformalEmpleadoMap: MultiTenantMap<GrupoInformalEmpleado>
    {
        public GrupoInformalEmpleadoMap()
        {
            //Atributos

            Property(gr => gr.FechaIngreso).IsRequired();

            //Llaves Foraneas

            //Tabla
            ToTable("grupo_informal_empleado");
        }
    }
}