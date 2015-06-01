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
    public class RespuestaMap : MultiTenantMap<Respuesta>
    {
        public RespuestaMap()
        {
            //Atributos
            Property(d => d.Texto).HasMaxLength(4096);
            Property(d => d.Texto).IsRequired();

            Property(d => d.Comodin50_50).IsRequired();

            Property(d => d.RespuestaVerdadera).IsRequired();

            //Llaves Foráneas

            //Tabla
            ToTable("respuesta");
        }
    }
}
