using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class FunerariaProspecto : EntidadMultiTenant
    {
        public string Nombre { get; set; }

        public virtual ICollection<GestionProspecto> Funeraria { get; set; }

        public FunerariaProspecto()
        {
            Funeraria = new List<GestionProspecto>();
        }
    }
}
