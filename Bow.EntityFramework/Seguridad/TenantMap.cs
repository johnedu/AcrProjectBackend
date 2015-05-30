using Bow.Seguridad.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Seguridad
{
    public class TenantMap : EntityTypeConfiguration<Tenant>
    {
        public TenantMap()
        {
            //Property(tenant => tenant.FechaLimiteUso).IsOptional();
        }
    }
}
