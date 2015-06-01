using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class Dimension : EntidadMultiTenant
    {
        public string Nombre { get; set; }

        public virtual ICollection<Pregunta> PreguntasDimension { get; set; }
        public virtual ICollection<Entidad> EntidadesDimension { get; set; }

        public Dimension()
        {
            PreguntasDimension = new List<Pregunta>();
            EntidadesDimension = new List<Entidad>();
        }
    }
}
