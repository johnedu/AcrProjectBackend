using System.Reflection;
using Abp.Modules;

namespace Bow
{
    public class BowCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
