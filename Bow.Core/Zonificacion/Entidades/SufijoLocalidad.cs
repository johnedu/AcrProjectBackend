using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class SufijoLocalidad : EntidadMultiTenant
    {
        public int SufijoId { get; set; }
        public Sufijo SufijoSufijoLocalidad { get; set; }
        public int LocalidadId { get; set; }
        public Localidad LocalidadSufijoLocalidad { get; set; }
        public virtual ICollection<Manzana> ManzanasSufijosLocalidad1 { get; set; }
        public virtual ICollection<Manzana> ManzanasSufijosLocalidad2 { get; set; }
        public virtual ICollection<Direccion> DireccionesSufijosLocalidad1 { get; set; }
        public virtual ICollection<Direccion> DireccionesSufijosLocalidad2 { get; set; }

        public SufijoLocalidad()
        {
            ManzanasSufijosLocalidad1 = new List<Manzana>();
            ManzanasSufijosLocalidad2 = new List<Manzana>();
            DireccionesSufijosLocalidad1 = new List<Direccion>();
            DireccionesSufijosLocalidad2 = new List<Direccion>();
        }
    }
}
