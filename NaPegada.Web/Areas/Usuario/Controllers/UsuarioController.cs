using NaPegada.Business;
using NaPegada.Web.Models;
using System.Web.Mvc;

namespace NaPegada.Web.Areas.User.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {

        #region [GLOBAIS, METODOS e CONSTRUTOR]
        private readonly UsuarioBUS _usuarioBUS;
        private readonly AutenticarController _authController;

        public UsuarioController()
        {
            _usuarioBUS = new UsuarioBUS();
            _authController = new AutenticarController();
            _usuarioBUS.Mensagem += CriarTempData;
        }

        [NonAction]
        private void CriarTempData(string tipo, string msg)
        {
            TempData[tipo] = msg;
        }
        #endregion

        #region [ACTIONS]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Registrar(UsuarioViewModel usuarioVM)
        {
            if (ModelState.IsValid)
            {
                _usuarioBUS.Registrar(usuarioVM.Usuario);
                return RedirectToAction("Entrar");
            }
            return RedirectToAction("Registrar");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Entrar(UsuarioViewModel usuarioVM)
        {
            if (ModelState.IsValid && _authController.Logar(usuarioVM))
                return View("Home", new UsuarioViewModel { Usuario = _usuarioBUS.ObterPorEmail(usuarioVM.Usuario.Email) });

            return RedirectToAction("Entrar");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Sair()
        {
            _authController.Deslogar();
            return RedirectToAction("Entrar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AtualizarUsuario(UsuarioViewModel usuarioVM, string id)
        {
            _usuarioBUS.Atualizar(usuarioVM.Usuario, id);
            return View("Home", new UsuarioViewModel { Usuario = _usuarioBUS.ObterPorId(id) });
        }

        [HttpPost]
        public ActionResult Pesquisar(string dadosPesquisa)
        {
            return View("_Pesquisar", new UsuarioViewModel { ResultadoPesquisa = _usuarioBUS.Pesquisar(dadosPesquisa) });
        }

        #endregion

        #region [VIEWS]

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Registrar()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Entrar()
        {
            return View();
        }

        [HttpGet]
        public ViewResult MeuPerfil(string id)
        {
            return View(new UsuarioViewModel { Usuario = _usuarioBUS.ObterPorId(id) });
        }

        [HttpGet]
        public ViewResult Perfil(string id)
        {
            return View(new UsuarioViewModel { Usuario = _usuarioBUS.ObterPorId(id) });
        }

        #endregion
    }
}
