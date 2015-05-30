using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.Entidades
{
    public class Parametro : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Tipo> Tipos { get; set; }
        public virtual ICollection<Estado> Estados { get; set; }

        public Parametro()
        {
            Tipos = new List<Tipo>();
            Estados = new List<Estado>();
        }

    }
}
