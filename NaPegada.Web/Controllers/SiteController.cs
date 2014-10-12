using System.Threading.Tasks;
using System.Web.Mvc;

namespace NaPegada.Web.Controllers
{
    [RoutePrefix("Site")]
    public class SiteController : BaseAsyncController
    {
        [HttpGet]
        //[OutputCache(Duration = 86400)]
        public async Task<ViewResult> Home()
        {
            return await Task.Run(() => View());
        }
    }
}