using System.Threading.Tasks;
using System.Web.Mvc;

namespace NaPegada.Web.Controllers
{
    public class SiteController : Controller
    {
        public async Task<ViewResult> Home()
        {
            return await Task.Run(() => View());
        }
    }
}