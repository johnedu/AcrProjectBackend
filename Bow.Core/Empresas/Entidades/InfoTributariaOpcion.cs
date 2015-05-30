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
    public class InfoTributariaOpcion : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int InfoTributariaId { get; set; }
        public virtual InfoTributaria InfoTributaria { get; set; }

        public virtual ICollection<EmpresaInfoTributaria> InfoTributariaOpcionEmpresas { get; set; }

        public InfoTributariaOpcion()
        {
            InfoTributariaOpcionEmpresas = new List<EmpresaInfoTributaria>();
        }
    }
}