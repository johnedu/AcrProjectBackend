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
    public class PreguntaFrecuenteMap : MultiTenantMap<PreguntaFrecuente>
    {
        public PreguntaFrecuenteMap()
        {
            //Atributos
            Property(faq => faq.Pregunta).HasMaxLength(4096);
            Property(faq => faq.Pregunta).IsRequired();

            Property(faq => faq.Respuesta).HasMaxLength(4096);
            Property(faq => faq.Respuesta).IsRequired();

            Property(faq => faq.EstadoActiva).IsRequired();

            //Tabla
            ToTable("pregunta_frecuente");
        }
    }
}
