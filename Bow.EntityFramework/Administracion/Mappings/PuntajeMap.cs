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
    public class PuntajeMap : MultiTenantMap<Puntaje>
    {
        public PuntajeMap()
        {
            //Atributos
            Property(d => d.PuntajeValor).IsRequired();

            Property(d => d.Respuesta).HasMaxLength(4096);
            Property(d => d.Respuesta).IsRequired();

            //Llaves Foráneas

            //Tabla
            ToTable("puntaje");
        }
    }
}
