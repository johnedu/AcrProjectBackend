using Bow.EntidadBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Entidades
{
    public class EmpresaTelefono : EntidadMultiTenant
    {
        public int EmpresaId { get; set; }
        public virtual Empresa EmpresaEmpresaTelefono { get; set; }
        public int TelefonoId { get; set; }
        public virtual Telefono TelefonoEmpresaTelefono { get; set; }
    }
}