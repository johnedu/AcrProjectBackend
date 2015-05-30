using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Entidades
{
    public class InfoTributariaLocalidad : EntidadMultiTenant
    {
        public int InfoTributariaId { get; set; }
        public virtual InfoTributaria InfoTributaria { get; set; }
        public int LocalidadId { get; set; }
        public virtual Localidad Localidad { get; set; }
    }
}