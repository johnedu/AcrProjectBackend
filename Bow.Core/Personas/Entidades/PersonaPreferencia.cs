using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Entidades
{
    public class PersonaPreferencia : EntidadMultiTenant
    {
        public int PersonaId { get; set; }
        public Persona PersonaPersonaPreferencia { get; set; }
        public int OpcionPreferenciaId { get; set; }
        public OpcionPreferencia OpcionPreferenciaPersonaPreferencia { get; set; }
    }
}
