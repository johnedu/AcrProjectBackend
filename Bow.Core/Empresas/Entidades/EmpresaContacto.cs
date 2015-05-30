using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using Bow.Personas.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Entidades
{
    public class EmpresaContacto : EntidadMultiTenant
    {
        public int EmpresaId { get; set; }
        public virtual Empresa EmpresaEmpresaContacto { get; set; }
        public int PersonaId { get; set; }
        public virtual Persona PersonaEmpresaContacto { get; set; }
        public int TipoAreaEmpresaId { get; set; }
        public virtual Tipo TipoAreaEmpresaContacto { get; set; }
        public string Cargo { get; set; }

    }
}