using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Bow.Seguridad.Autorizacion;
using Bow.Seguridad.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Seguridad.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        public TenantManager(IRepository<Tenant> tenantRepository) : base(tenantRepository)
        {

        }
    }
}
