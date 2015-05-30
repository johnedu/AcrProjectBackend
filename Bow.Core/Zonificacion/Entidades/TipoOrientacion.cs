using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class TipoOrientacion : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public virtual ICollection<TorieLocalidad> TorieLocalidades { get; set; }

        public TipoOrientacion()
        {
            TorieLocalidades = new List<TorieLocalidad>();
        }
    }
}
