using Abp.Authorization;
using Bow.Seguridad.MultiTenancy;
using Bow.Seguridad.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Seguridad.Autorizacion
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager) : base(userManager)
        {

        }
    }
}
