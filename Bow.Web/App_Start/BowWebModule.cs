using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Localization;
using Abp.Localization.Sources.Xml;
using Abp.Modules;
using Abp.Localization.Sources.Resource;
using Abp.Zero.Configuration;

namespace Bow.Web
{
    [DependsOn(typeof(BowDataModule), typeof(BowApplicationModule), typeof(BowWebApiModule))]
    public class BowWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("es", "Español", "famfamfam-flag-co", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-us"));

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new ResourceFileLocalizationSource(
                    "Bow",
                    Bow.ResourceManager
                    )
                );

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<BowNavigationProvider>();

            //Habilitando Multy-Tenancy
            Configuration.MultiTenancy.IsEnabled = true;

            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
