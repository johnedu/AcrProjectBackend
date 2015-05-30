using Bow.MappingsBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Mappings
{
    public class OpcionPreferenciaMap : MultiTenantMap<OpcionPreferencia>
    {
        public OpcionPreferenciaMap()
        {
            //Atributos
            Property(preferencia => preferencia.Nombre).HasMaxLength(80);

            //Llaves Foraneas
            HasMany<PersonaPreferencia>(opcionPreferencia => opcionPreferencia.PersonaPreferencia)
            .WithRequired(opcionPreferenciaPersonaPreferencia => opcionPreferenciaPersonaPreferencia.OpcionPreferenciaPersonaPreferencia)
            .HasForeignKey(opcionPreferenciaPersonaPreferencia => opcionPreferenciaPersonaPreferencia.OpcionPreferenciaId)
            .WillCascadeOnDelete(false);

            //Tabla
            ToTable("opcion_preferencia");
        }
    }
}
