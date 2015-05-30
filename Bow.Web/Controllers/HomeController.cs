using Abp.Web.Mvc.Authorization;
using System.Web.Mvc;

namespace Bow.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : BowControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}