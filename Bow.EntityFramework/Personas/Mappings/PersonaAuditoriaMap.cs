using Bow.MappingsBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Mappings
{
    public class PersonaAuditoriaMap : MultiTenantMap<PersonaAuditoria>
    {
        public PersonaAuditoriaMap()
        {
            Property(personaauditoria => personaauditoria.Cambios).HasMaxLength(3000);
            Property(personaauditoria => personaauditoria.Cambios).IsRequired();

            //Tabla
            ToTable("persona_auditoria");
        }
    }
}
