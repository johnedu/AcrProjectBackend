using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class TorieLocalidad : EntidadMultiTenant
    {
        public int TipoOrientacionId { get; set; }
        public TipoOrientacion TipoOrientacionTorieLocalidad { get; set; }
        public int LocalidadId { get; set; }
        public Localidad LocalidadTorieLocalidad { get; set; }

        public virtual ICollection<Manzana> ManzanasTorieLocalidad1 { get; set; }
        public virtual ICollection<Manzana> ManzanasTorieLocalidad2 { get; set; }
        public virtual ICollection<Direccion> DireccionesTorieLocalidad1 { get; set; }
        public virtual ICollection<Direccion> DireccionesTorieLocalidad2 { get; set; }

        public TorieLocalidad()
        {
            ManzanasTorieLocalidad1 = new List<Manzana>();
            ManzanasTorieLocalidad2 = new List<Manzana>();
            DireccionesTorieLocalidad1 = new List<Direccion>();
            DireccionesTorieLocalidad2 = new List<Direccion>();
        }
    }
}
