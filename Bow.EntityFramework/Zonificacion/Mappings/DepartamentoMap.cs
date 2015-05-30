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
    public class DepartamentoMap : MultiTenantMap<Departamento>
    {
        
        public DepartamentoMap()
        {
            //Atributos
            Property(departamento => departamento.Nombre).HasMaxLength(25);
            Property(departamento => departamento.Nombre).IsRequired();
            Property(departamento => departamento.Indicativo).HasMaxLength(4);
            Property(departamento => departamento.Indicativo).IsRequired();

            //Laves Foráneas
            HasMany<Localidad>(departamento => departamento.Localidades)
                .WithRequired(localidad => localidad.DepartamentoLocalidad)
                .HasForeignKey(localidad => localidad.DepartamentoId)
                .WillCascadeOnDelete(false);

            //Tabla
            ToTable("departamento");
        }
    }
}
