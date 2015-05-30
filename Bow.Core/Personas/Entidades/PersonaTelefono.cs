using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Entidades
{
    public class PersonaTelefono : EntidadMultiTenant
    {
        public int PersonaId { get; set; }
        public Persona PersonaPersonaTelefono { get; set; }
        public int TelefonoId { get; set; }
        public Telefono TelefonoPersonaTelefono { get; set; }
        public int TipoUbicacionId { get; set; }
        public Tipo TipoUbicacion { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public string UsuarioCancelacion { get; set; }
    }
}
