using Bow.MappingsBase;
using Bow.Personas.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Mappings
{
    public class PaisMap : MultiTenantMap<Pais>
    {
        public PaisMap()
        {
            //Atributos
            Property(pais => pais.Nombre).HasMaxLength(25);
            Property(pais => pais.Nombre).IsRequired();
            Property(pais => pais.Indicativo).HasMaxLength(4);
            Property(pais => pais.Indicativo).IsRequired();

            //Llaves Foráneas
            HasMany<Departamento>(pais => pais.Departamentos)
                .WithRequired(departamento => departamento.PaisDepartamento)
                .HasForeignKey(departamento => departamento.PaisId)
                .WillCascadeOnDelete(false);

            //Llaves Foráneas
            HasMany<Persona>(pais => pais.Personas)
                .WithRequired(persona => persona.PersonaPais)
                .HasForeignKey(persona => persona.PaisId)
                .WillCascadeOnDelete(false);

            //Llaves Foráneas
            HasMany<TipoDocumentoPersona>(pais => pais.TipoDocumentoPersonas)
                .WithRequired(tipodocumentopersona => tipodocumentopersona.TipoDocumentoPersonaPais)
                .HasForeignKey(tipodocumentopersona => tipodocumentopersona.PaisId)
                .WillCascadeOnDelete(false);

            //Tabla
            ToTable("pais");
        }
    }
}
