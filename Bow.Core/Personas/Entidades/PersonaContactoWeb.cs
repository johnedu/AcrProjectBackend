using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Entidades
{
    public class PersonaContactoWeb : EntidadMultiTenant
    {
        public int PersonaId { get; set; }
        public Persona PersonaPersonaContactoWeb { get; set; }
        public int TipoId { get; set; }
        public Tipo TipoPersonaContactoWeb { get; set; }
        public string Identificador { get; set; }
    }
}
