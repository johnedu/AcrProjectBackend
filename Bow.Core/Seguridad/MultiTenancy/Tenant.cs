using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MultiTenancy;
using Bow.Seguridad.Usuarios;
using System.ComponentModel.DataAnnotations.Schema;
using Bow.Empresas.Entidades;

namespace Bow.Seguridad.MultiTenancy
{
    public class Tenant : AbpTenant<Tenant, User>
    {
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        protected Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name) : base(tenancyName, name)
        {

        }
    }
}
