using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class Sufijo : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public virtual ICollection<SufijoLocalidad> SufijosLocalidades { get; set; }

        public Sufijo()
        {
            SufijosLocalidades = new List<SufijoLocalidad>();
        }
    }
}
