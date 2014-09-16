using NaPegada.Business;
using NaPegada.Model;
using NaPegada.Web.Models;
using System.Web.Mvc;

namespace NaPegada.Web.Areas.User.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {

        #region [GLOBAIS, METODOS e CONSTRUTOR]
        private readonly UsuarioBUS _usuarioBUS;

        public UsuarioController()
        {
            _usuarioBUS = new UsuarioBUS();
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
                _usuarioBUS.Registrar(new UsuarioMOD
                {
                    Nome = usuarioVM.Nome,
                    Email = usuarioVM.Email,
                    Senha = usuarioVM.Senha
                });

                return RedirectToAction("Entrar");
            }
            return RedirectToAction("Registrar");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Entrar(UsuarioViewModel usuarioVM)
        {
            if (ModelState.IsValid && _usuarioBUS.Logar(new UsuarioMOD
            {
                Email = usuarioVM.Email,
                Senha = usuarioVM.Senha

            }, usuarioVM.ManterConectado))
                return View("Home", new UsuarioViewModel(_usuarioBUS.ObterPorEmail(usuarioVM.Email)));

            return RedirectToAction("Entrar");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Sair()
        {
            _usuarioBUS.Deslogar();
            return RedirectToAction("Entrar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AtualizarUsuario(UsuarioViewModel usuarioVM)
        {
            _usuarioBUS.Atualizar(new UsuarioMOD
            {
                Id = _usuarioBUS.ConverterParaObjectId(usuarioVM.Id),
                Nome = usuarioVM.Nome,
                Email = usuarioVM.Email,
                Senha = usuarioVM.Senha,
                NomeFotoPerfil = usuarioVM.NomeFotoPerfil,
                Endereco = new EnderecoMOD
                {
                    Cep = usuarioVM.Cep,
                    Bairro = usuarioVM.Bairro,
                    Localidade = usuarioVM.Localidade,
                    Logradouro = usuarioVM.Logradouro,
                    Numero = usuarioVM.Numero,
                    Uf = usuarioVM.Uf
                }
            }, usuarioVM.Upload);

            return View("MeuPerfil", new UsuarioViewModel(_usuarioBUS.ObterPorId(usuarioVM.Id)));
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
            return View(new UsuarioViewModel(_usuarioBUS.ObterPorId(id)));
        }

        [HttpGet]
        public ViewResult Home(string id)
        {
            return View(new UsuarioViewModel(_usuarioBUS.ObterPorId(id)));
        }

        #endregion
    }
}
