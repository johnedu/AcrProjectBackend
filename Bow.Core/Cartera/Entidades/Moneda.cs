using Bow.Afiliaciones.Entidades;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Cartera.Entidades
{
    public class Moneda : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Simbolo { get; set; }

        public virtual ICollection<PlanExequial> PlanesExequiales { get; set; }

        public Moneda()
        {
            PlanesExequiales = new List<PlanExequial>();
        }
    }
}