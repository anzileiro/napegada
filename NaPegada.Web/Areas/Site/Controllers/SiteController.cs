using System.Web.Mvc;

namespace NaPegada.Web.Areas.Site.Controllers
{
    public class SiteController : Controller
    {
        [HttpGet]
        public ViewResult Home()
        {
            return View();
        }
    }
}
