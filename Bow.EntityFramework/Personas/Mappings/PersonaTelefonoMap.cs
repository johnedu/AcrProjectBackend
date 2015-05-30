using Bow.MappingsBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Mappings
{
    public class PersonaTelefonoMap : MultiTenantMap<PersonaTelefono>
    {
        public PersonaTelefonoMap()
        {
            //Atributos
            Property(personaTelefono => personaTelefono.FechaIngreso).IsRequired();
            Property(personaTelefono => personaTelefono.FechaCancelacion).IsOptional();

            //Tabla
            ToTable("persona_telefono");
        }
    }
}
