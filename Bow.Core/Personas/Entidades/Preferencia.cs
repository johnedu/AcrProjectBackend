using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Entidades
{
    public class Preferencia : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado EstadoPreferencia { get; set; }
        public virtual ICollection<OpcionPreferencia> OpcionesPreferencias { get; set; }

        public Preferencia()
        {
            OpcionesPreferencias = new List<OpcionPreferencia>();
        }
    }
}
