using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class Barrio : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int LocalidadId { get; set; }
        public virtual Localidad Localidad { get; set; }
        public virtual ICollection<Manzana> Manzanas { get; set; }
        public virtual ICollection<Direccion> Direcciones { get; set; }
        public virtual ICollection<ZonaBarrio> ZonasBarrios { get; set; }

        public Barrio()
        {
            ZonasBarrios = new List<ZonaBarrio>();
            Manzanas = new List<Manzana>();
            Direcciones = new List<Direccion>();
        }
    }
}
