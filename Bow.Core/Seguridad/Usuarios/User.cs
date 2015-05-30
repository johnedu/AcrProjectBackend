using Abp.Authorization.Users;
using Bow.Seguridad.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Seguridad.Usuarios
{
    public class User : AbpUser<Tenant, User>
    {
    }
}
