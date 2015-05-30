using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using Bow.EntityFramework;

namespace Bow
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(BowCoreModule))]
    public class BowDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<BowDbContext>(null);
        }
    }
}
