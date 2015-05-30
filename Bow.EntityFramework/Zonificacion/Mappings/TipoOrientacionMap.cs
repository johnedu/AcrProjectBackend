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
    public class TipoOrientacionMap : MultiTenantMap<TipoOrientacion>
    {
        public TipoOrientacionMap()
        {
            //Atributos
            Property(tipoOrientacion => tipoOrientacion.Nombre).HasMaxLength(20);
            Property(tipoOrientacion => tipoOrientacion.Nombre).IsRequired();

            //Llaves Foráneas
            HasMany<TorieLocalidad>(tipoOrientacion => tipoOrientacion.TorieLocalidades)
               .WithRequired(torieLocalidad => torieLocalidad.TipoOrientacionTorieLocalidad)
               .HasForeignKey(torieLocalidad => torieLocalidad.TipoOrientacionId)
               .WillCascadeOnDelete(false);

            //Tabla
            ToTable("tipo_orientacion");

        }
    }
}
