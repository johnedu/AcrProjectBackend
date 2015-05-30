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
    public class BarrioMap : MultiTenantMap<Barrio>
    {
        public BarrioMap()
        {
            //Atributos
            Property(barrio => barrio.Nombre).HasMaxLength(40);
            Property(barrio => barrio.Nombre).IsRequired();

            //Llaves Foráneas         
          
            HasMany<Manzana>(barrio => barrio.Manzanas)
                .WithRequired(manzana => manzana.BarrioManzana)
                .HasForeignKey(manzana => manzana.BarrioId)
                .WillCascadeOnDelete(false);

            HasMany<Direccion>(barrio => barrio.Direcciones)
                .WithOptional(direccion => direccion.BarrioDireccion)
                .HasForeignKey(direccion => direccion.BarrioId)
                .WillCascadeOnDelete(false);

            HasMany<ZonaBarrio>(barrio => barrio.ZonasBarrios)
                 .WithRequired(zonaBarrio => zonaBarrio.BarrioZonaBarrio)
                 .HasForeignKey(zonaBarrio => zonaBarrio.BarrioId);

            //Tabla
            ToTable("barrio");
        }
    }
}
