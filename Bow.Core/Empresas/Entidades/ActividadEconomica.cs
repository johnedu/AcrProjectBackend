using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Entidades
{
    public class ActividadEconomica : EntidadMultiTenant
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Empresa> Empresas { get; set; }

        public ActividadEconomica()
        {
            Empresas = new List<Empresa>();
        }
    }
}
