using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class Avenida : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int LocalidadId { get; set; }
        public Localidad LocalidadAvenida { get; set; }
    }
}
