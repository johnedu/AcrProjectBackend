using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Entidades
{
    public class PersonaAuditoria : EntidadMultiTenant
    {
        public int PersonaId { get; set; }
        public Persona PersonaPersonaAuditoria { get; set; }
        public DateTime FechaCambio { get; set; }
        public string Usuario  { get; set; }
        public string Cambios { get; set; }
    }
}
