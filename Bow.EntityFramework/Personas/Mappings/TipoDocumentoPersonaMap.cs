using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Mappings
{
   public class TipoDocumentoPersonaMap: MultiTenantMap<TipoDocumentoPersona>
    {
       public TipoDocumentoPersonaMap()
        {
            //Atributos
            Property(persona => persona.Nombre).HasMaxLength(30);
            Property(persona => persona.Nombre).IsRequired();

            Property(persona => persona.LongitudMaxima).IsRequired();

            Property(persona => persona.LongitudMinima).IsRequired();

            Property(persona => persona.ConjuntoCaracteres).HasMaxLength(1);
            Property(persona => persona.ConjuntoCaracteres).IsRequired();

            Property(persona => persona.EdadMinima).IsOptional();

            Property(persona => persona.EdadMaxima).IsOptional();

            Property(persona => persona.Default).HasMaxLength(1);
            Property(persona => persona.Default).IsRequired();

            Property(persona => persona.AplicaEmpresa).HasMaxLength(1);
            Property(persona => persona.AplicaEmpresa).IsRequired();

            Property(persona => persona.AplicaPersona).HasMaxLength(1);
            Property(persona => persona.AplicaPersona).IsRequired();

            ////Llaves Foráneas
            HasMany<Persona>(tipodocumentopersona => tipodocumentopersona.Personas)
                .WithOptional(persona => persona.PersonaTipoDocumentoPersona)
                .HasForeignKey(persona => persona.TipoDocumentoId)
                .WillCascadeOnDelete(false);

            HasMany<Empresa>(tipodocumentoempresa => tipodocumentoempresa.Empresas)
                 .WithRequired(tipodocumentoempresa => tipodocumentoempresa.TipoDocumento)
                 .HasForeignKey(tipodocumentoempresa => tipodocumentoempresa.TipoDocumentoId)
                 .WillCascadeOnDelete(false);

            //Tabla
            ToTable("tipo_documento_persona");
        }
    }
}