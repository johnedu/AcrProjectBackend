using Bow.Cartera.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class GrupoParentescoRango : EntidadMultiTenant
    {
        public int GrupoFamiliarParentescoId { get; set; }
        public virtual GrupoFamiliarParentesco GrupoFamiliarParentesco { get; set; }
        public int EdadMinima { get; set; }
        public int EdadMaxima { get; set; }
        public int PeriodoCarencia { get; set; }
        public string UnidadPeriodoCarencia { get; set; }
        public string TipoValorBasico { get; set; }
        public int ValorBasico { get; set; }
        public string TipoValorAdicional { get; set; }
        public int ValorAdicional { get; set; }
    }
}