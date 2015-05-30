using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class Departamento : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Indicativo { get; set; }
        public int PaisId { get; set; }
        public virtual Pais PaisDepartamento { get; set; }
        public virtual ICollection<Localidad> Localidades { get; set; }

        public Departamento()
        {
            Localidades = new List<Localidad>();
        }
    }
}
