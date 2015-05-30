using Bow.EntidadBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class Prospecto : EntidadMultiTenant
    {
        public int? DireccionId { get; set; }
        public virtual Direccion Direccion { get; set; }
        public int? TelefonoId { get; set; }
        public virtual Telefono Telefono { get; set; }

        public virtual ICollection<GestionProspecto> GestionProspecto { get; set; }

        public Prospecto()
        {
            GestionProspecto = new List<GestionProspecto>();
        }
    }
}
