using Bow.MappingsBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Mappings
{
    public class PersonaPreferenciaMap: MultiTenantMap<PersonaPreferencia>
    {
        public PersonaPreferenciaMap()
        {
           
            //Tabla
            ToTable("persona_preferencia");
        }
    }
}