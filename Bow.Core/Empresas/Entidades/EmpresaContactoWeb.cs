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
    public class EmpresaContactoWeb : EntidadMultiTenant
    {
        public int EmpresaId { get; set; }
        public virtual Empresa EmpresaEmpresaContactoWeb { get; set; }
        public int TipoRedId { get; set; }
        public virtual Tipo TipoRedEmpresaContactoWeb { get; set; }
        public string Identificador { get; set; }
    }
}