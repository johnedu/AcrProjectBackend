using Bow.MappingsBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.Mappings
{
    public class NombreEstadoMap : MultiTenantMap<NombreEstado>
    {
        public NombreEstadoMap()
        {
            //Atributos
            Property(nombreestado => nombreestado.Nombre).HasMaxLength(20);
            Property(nombreestado => nombreestado.Nombre).IsRequired();
            Property(nombreestado => nombreestado.Abreviacion).HasMaxLength(2);
            Property(nombreestado => nombreestado.Abreviacion).IsRequired();

            //Llaves Foráneas
            HasMany<Estado>(nombreestado => nombreestado.Estados)
                .WithRequired(estado => estado.EstadoNombreEstado)
                .HasForeignKey(estado => estado.EstadoNombreId);

            //Tabla
            ToTable("nombre_estado");
        }
    }
}
