using Bow.MappingsBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Mappings
{
    public class PersonaContactoWebMap: MultiTenantMap<PersonaContactoWeb>
    {
        public PersonaContactoWebMap()
        {
            //Atributos
            Property(personaContactoWeb => personaContactoWeb.Identificador).IsOptional();

            ToTable("persona_contacto_web");
        }
    }
}
