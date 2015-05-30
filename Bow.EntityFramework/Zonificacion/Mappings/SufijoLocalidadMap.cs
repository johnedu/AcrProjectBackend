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
    public class SufijoLocalidadMap : MultiTenantMap<SufijoLocalidad>
    {
        public SufijoLocalidadMap()
        {
            //Llaves Foráneas
            HasMany<Manzana>(sufijoLocalidad => sufijoLocalidad.ManzanasSufijosLocalidad1)
                .WithOptional(manzana => manzana.SufijoLocalidad1)
                .HasForeignKey(manzana => manzana.SufijoLocalidad1Id)
                .WillCascadeOnDelete(false);

            HasMany<Manzana>(sufijoLocalidad => sufijoLocalidad.ManzanasSufijosLocalidad2)
                .WithOptional(manzana => manzana.SufijoLocalidad2)
                .HasForeignKey(manzana => manzana.SufijoLocalidad2Id)
                .WillCascadeOnDelete(false);

            HasMany<Direccion>(sufijoLocalidad => sufijoLocalidad.DireccionesSufijosLocalidad1)
                .WithOptional(direccion => direccion.SufijoLocalidad1)
                .HasForeignKey(direccion => direccion.SufijoLocalidad1Id)
                .WillCascadeOnDelete(false);

            HasMany<Direccion>(sufijoLocalidad => sufijoLocalidad.DireccionesSufijosLocalidad2)
                .WithOptional(direccion => direccion.SufijoLocalidad2)
                .HasForeignKey(direccion => direccion.SufijoLocalidad2Id)
                .WillCascadeOnDelete(false);

            //Tabla
            ToTable("sufijo_localidad");
        }
    }
}
