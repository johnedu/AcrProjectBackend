using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.EntidadBase
{
    public abstract class EntidadMultiTenant : Entity, IMultiTenant, IMustHaveTenant
    {
        public int TenantId { get; set; }
    }
}
