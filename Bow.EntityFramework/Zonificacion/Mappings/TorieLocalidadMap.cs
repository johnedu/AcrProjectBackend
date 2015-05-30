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
    public class TorieLocalidadMap : MultiTenantMap<TorieLocalidad>
    {
        public TorieLocalidadMap()
        {
            //Llaves Foráneas
            HasMany<Manzana>(torieLocalidad => torieLocalidad.ManzanasTorieLocalidad1)
                .WithOptional(manzana => manzana.TorieLocalidad1)
                .HasForeignKey(manzana => manzana.TorieLocalidad1Id)
                .WillCascadeOnDelete(false);

            HasMany<Manzana>(torieLocalidad => torieLocalidad.ManzanasTorieLocalidad2)
                .WithOptional(manzana => manzana.TorieLocalidad2)
                .HasForeignKey(manzana => manzana.TorieLocalidad2Id)
                .WillCascadeOnDelete(false);

            HasMany<Direccion>(torieLocalidad => torieLocalidad.DireccionesTorieLocalidad1)
                .WithOptional(direccion => direccion.TorieLocalidad1)
                .HasForeignKey(direccion => direccion.TorieLocalidad1Id)
                .WillCascadeOnDelete(false);

            HasMany<Direccion>(torieLocalidad => torieLocalidad.DireccionesTorieLocalidad2)
                .WithOptional(direccion => direccion.TorieLocalidad2)
                .HasForeignKey(direccion => direccion.TorieLocalidad2Id)
                .WillCascadeOnDelete(false);

            //Tabla
            ToTable("torie_localidad");
        }
    }
}
