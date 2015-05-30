using Bow.MappingsBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Mappings
{
    public class PersonaDireccionMap: MultiTenantMap<PersonaDireccion>
    {
        public PersonaDireccionMap()
        {
            //Atributos
            Property(personaDireccion => personaDireccion.FechaIngreso).IsRequired();
            Property(personaDireccion => personaDireccion.FechaCancelacion).IsOptional();

            //Tabla
            ToTable("persona_direccion");
        }
    }
}
