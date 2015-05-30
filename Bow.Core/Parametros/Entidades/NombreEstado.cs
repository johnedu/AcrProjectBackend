using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.Entidades
{
   public class NombreEstado : EntidadMultiTenant
    {
        public string Abreviacion { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Estado> Estados { get; set; }

        public NombreEstado()
        {
            Estados = new List<Estado>();
        }
    }
}
