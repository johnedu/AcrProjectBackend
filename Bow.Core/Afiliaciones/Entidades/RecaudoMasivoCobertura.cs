using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
   public class RecaudoMasivoCobertura: EntidadMultiTenant
    {
        public int RecaudoMasivoId { get; set; }
        public RecaudoMasivo RecaudoMasivoCoberturaRecaudoMasivo { get; set; }
        public int LocalidadId { get; set; }
        public Localidad LocalidadRecaudoMasivo { get; set; }
    }
}
