using NaPegada.Business;
using NaPegada.Repository;
using NaPegada.Web.Models.Site;
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
            
            var userBus = new UsuarioBUS(new UsuarioREP());

            var listaTodasDoacoes = await userBus.ObterTodasDoacoes();
            var model = new HomeViewModel(listaTodasDoacoes);

            return await Task.Run(() => View(model));
        }

        public ActionResult Parceiros()
        {
            return View();
        }

        public ActionResult Historia()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

    }
}