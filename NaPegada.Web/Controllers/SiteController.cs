using NaPegada.Business;
using NaPegada.Repository;
using NaPegada.Web.Models.Doacao;
using NaPegada.Web.Models.Site;
using NaPegada.Web.Models.Usuario;
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

        public async Task<PartialViewResult> Detalhes(string id)
        {
            var model = default(DoacaoViewModel);

                var userBus = new UsuarioBUS(new UsuarioREP());
                var doacao = await userBus.ObterDoacao(id);
                model = new DoacaoViewModel(doacao);

            return PartialView("_DescricaoAnimal", model);
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