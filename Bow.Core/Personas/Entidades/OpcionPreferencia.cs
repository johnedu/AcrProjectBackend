using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Entidades
{
    public class OpcionPreferencia : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int PreferenciaId { get; set; }
        public virtual Preferencia PreferenciaOpcion { get; set; }
        public virtual ICollection<PersonaPreferencia> PersonaPreferencia { get; set; }


        public OpcionPreferencia()
        {
            PersonaPreferencia = new List<PersonaPreferencia>();
        }
    }
}
