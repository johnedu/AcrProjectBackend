using Abp.Application.Services;

namespace Bow
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class BowAppServiceBase : ApplicationService
    {
        protected BowAppServiceBase()
        {
            LocalizationSourceName = BowConsts.LocalizationSourceName;
        }
    }
}