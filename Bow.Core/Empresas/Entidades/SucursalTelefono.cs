using Bow.EntidadBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Entidades
{
    public class SucursalTelefono : EntidadMultiTenant
    {
        public int SucursalId { get; set; }
        public virtual Sucursal SucursalSucursalTelefono { get; set; }
        public int TelefonoId { get; set; }
        public virtual Telefono TelefonoSucursalTelefono { get; set; }
    }
}