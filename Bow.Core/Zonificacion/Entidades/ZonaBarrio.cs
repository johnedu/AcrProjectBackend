using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
   public class ZonaBarrio : EntidadMultiTenant
    {
        public int ZonaId { get; set; }
        public Zona ZonaZonaBarrio { get; set; }
        public int BarrioId { get; set; }
        public Barrio BarrioZonaBarrio { get; set; }
    }
}

