using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Bow
{
    [DependsOn(typeof(AbpWebApiModule), typeof(BowApplicationModule))]
    public class BowWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(BowApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
