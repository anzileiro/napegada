using NaPegada.Business;
using NaPegada.Web.Models;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NaPegada.Web.Controllers
{
    [Autenticar]
    [RoutePrefix("Usuario")]
    public class UsuarioController : AsyncController
    {
        private readonly UsuarioBUS _usuarioBUS;

        public UsuarioController()
        {
            _usuarioBUS = new UsuarioBUS();
        }

        #region [ViewResult]
        [HttpGet]
        [OutputCache(Duration = 86400)]
        public async Task<ViewResult> Home()
        {
            return await Task.Run(() => View(ObterUsuarioDaSecao().Result));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Sair")]
        public async Task<ActionResult> Sair()
        {
            LogOut();
            return await Task.Run(() => RedirectToAction("Home", "Site"));
        }

        [HttpGet]
        [OutputCache(Duration = 86400)]
        public async Task<ViewResult> MinhasDoacoes()
        {
            return await Task.Run(() => View());
        }

        [HttpGet]
        [OutputCache(Duration = 86400)]
        public async Task<ViewResult> MeusInteresses()
        {
            return await Task.Run(() => View());
        }
        #endregion

        #region [JsonResult]
        [HttpPost]
        [AllowAnonymous]
        [Route("Entrar")]
        public async Task<ActionResult> Entrar(UsuarioViewModel usuarioVM)
        {
            return await (await LogIn(usuarioVM) ? Task.Run(() => RedirectToAction("Home", "Usuario")) : Task.Run(() => RedirectToAction("Home", "Site")));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Registrar")]
        public async Task<JsonResult> Registrar(UsuarioViewModel usuarioVM)
        {
            return await Task.Run(() => Json(_usuarioBUS.Registrar(usuarioVM.Usuario)));
        }
        #endregion

        #region [NonAction]
        [NonAction]
        private async Task<bool> LogIn(UsuarioViewModel usuarioVM)
        {
            return (await _usuarioBUS.EhUsuario(usuarioVM.Usuario) ? Session["napegada_auth"] = usuarioVM : null) != null;
        }

        [NonAction]
        private void LogOut()
        {
            Session["napegada_auth"] = null;
        }

        [NonAction]
        private async Task<UsuarioViewModel> ObterUsuarioDaSecao()
        {
            return await Task.Run(() => Session["napegada_auth"] as UsuarioViewModel);
        }
        #endregion
    }
}