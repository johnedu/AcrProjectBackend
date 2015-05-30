using Bow.MappingsBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.Mappings
{
    public class ParametroMap : MultiTenantMap<Parametro>
    {
        public ParametroMap()
        {
            //Atributos
            Property(parametro => parametro.Nombre).HasMaxLength(80);
            Property(parametro => parametro.Nombre).IsRequired();
            Property(parametro => parametro.Descripcion).HasMaxLength(300);

            //Llaves Foráneas
            HasMany<Tipo>(parametro => parametro.Tipos)
                .WithRequired(tipo => tipo.ParametroTipo)
                .HasForeignKey(tipo => tipo.ParametroId)
                .WillCascadeOnDelete();

            HasMany<Estado>(parametro => parametro.Estados)
                .WithRequired(estado => estado.ParametroEstado)
                .HasForeignKey(estado => estado.ParametroId)
                .WillCascadeOnDelete();

            //Tabla
            ToTable("parametro");
        }
    }
}
