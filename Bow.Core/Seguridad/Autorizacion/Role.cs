using Abp.Authorization.Roles;
using Bow.Seguridad.MultiTenancy;
using Bow.Seguridad.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Seguridad.Autorizacion
{
    public class Role : AbpRole<Tenant, User>
    {
        public Role()
        {

        }

        public Role(int? tenantId, string name, string displayName) : base (tenantId, name, displayName)
        {

        }
    }
}
