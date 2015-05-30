using Bow.EntidadBase;
using Bow.Seguridad.MultiTenancy;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Entidades
{
    public class Organizacion : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<EmpresaOrganizacion> EmpresaOrganizacion { get; set; }
        public virtual ICollection<RecaudoMasivo> RecaudoMasivoOrganizacion { get; set; }
        public virtual ICollection<Tenant> TenantsOrganizacion { get; set; }

        public Organizacion()
        {
            EmpresaOrganizacion = new List<EmpresaOrganizacion>();
            RecaudoMasivoOrganizacion = new List<RecaudoMasivo>();
            TenantsOrganizacion = new List<Tenant>();
        }
    }
}