using Abp.Web.Mvc.Controllers;

namespace Bow.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class BowControllerBase : AbpController
    {
        protected BowControllerBase()
        {
            LocalizationSourceName = BowConsts.LocalizationSourceName;
        }
    }
}