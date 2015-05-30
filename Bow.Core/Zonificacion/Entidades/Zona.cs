using Abp.Domain.Entities;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class Zona : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int LocalidadId { get; set; }
        public Localidad LocalidadZona { get; set; }
        public int TipoId { get; set; }
        public Tipo TipoZona { get; set; }
        public virtual ICollection<ZonaBarrio> ZonasBarrios { get; set; }
        public virtual ICollection<ZonaEmpleado> ZonasEmpleado { get; set; }

        public Zona()
        {
            ZonasBarrios = new List<ZonaBarrio>();
            ZonasEmpleado = new List<ZonaEmpleado>();
        }
    }
}
