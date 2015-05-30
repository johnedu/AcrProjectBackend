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
    public class ManzanaMap : MultiTenantMap<Manzana>
    {
        public ManzanaMap()
        {
            //Atributos
            Property(manzana => manzana.Nombre).HasMaxLength(50);

            //Llaves Foráneas
            HasMany<Direccion>(manzana => manzana.Direcciones)
                .WithOptional(direccion => direccion.ManzanaDireccion)
                .HasForeignKey(direccion => direccion.ManzanaId)
                .WillCascadeOnDelete(false);

            //Tabla
            ToTable("manzana");
        }
    }
}
