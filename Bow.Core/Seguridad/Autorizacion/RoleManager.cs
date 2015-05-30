using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using Bow.Seguridad.MultiTenancy;
using Bow.Seguridad.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Seguridad.Autorizacion
{
    public class RoleManager : AbpRoleManager<Tenant, Role, User>
    {
        public RoleManager(RoleStore store, IPermissionManager permissionManager, IRoleManagementConfig roleManagementConfig, IUnitOfWorkManager unitOfWorkManager)
            :base (store, permissionManager, roleManagementConfig, unitOfWorkManager)
        {

        }
    }
}
