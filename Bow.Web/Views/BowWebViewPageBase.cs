using Abp.Web.Mvc.Views;

namespace Bow.Web.Views
{
    public abstract class BowWebViewPageBase : BowWebViewPageBase<dynamic>
    {

    }

    public abstract class BowWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected BowWebViewPageBase()
        {
            LocalizationSourceName = BowConsts.LocalizationSourceName;
        }
    }
}