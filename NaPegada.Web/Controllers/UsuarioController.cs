using NaPegada.Business;
using NaPegada.Web.Models;
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

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Home()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Entrar/{usuarioVM}")]
        public JsonResult Logar(UsuarioViewModel usuarioVM)
        {
            return Json(new { resposta = LogarComSecao(usuarioVM) });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Sair")]
        public ViewResult Deslogar()
        {
            DeslogarComSecao();
            return View("Entrar");
        }

        [NonAction]
        private bool LogarComSecao(UsuarioViewModel usuarioVM)
        {
            var secao = HttpContext.Session["napegada_auth"] = _usuarioBUS.EhUsuario(usuarioVM.Usuario) ? HttpContext.Session["napegada_auth"] = usuarioVM : null;
            return secao != null;
        }
        [NonAction]
        private void DeslogarComSecao()
        {
            HttpContext.Session["napegada_auth"] = null;
        }
    }
}