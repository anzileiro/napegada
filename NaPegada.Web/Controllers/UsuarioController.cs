using NaPegada.Business;
using NaPegada.Model;
using NaPegada.Web.Models;
using NaPegada.Web.Models.Usuario;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NaPegada.Web.Controllers
{
    [AutenticarAutorizar]
    [RoutePrefix("Usuario")]
    public class UsuarioController : BaseAsyncController
    {
        private readonly UsuarioBUS _usuarioBUS;
        private readonly RacaBUS _racaBUS;

        public UsuarioController()
        {
            _usuarioBUS = new UsuarioBUS();
        }

        #region [ViewResult]
        [HttpGet]
        //[OutputCache(Duration = 86400)]
        public async Task<ViewResult> Home()
        {
            var viewModel = new UsuarioViewModel();

            viewModel.Usuario = ObterUsuarioDaSecao();
            return await Task.Run(() => View(viewModel));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Sair")]
        public async Task<JsonResult> Sair()
        {
            LogOut();
            return await Task.Run(() => Json(new { url = "/Site/Home" }, JsonRequestBehavior.AllowGet));
        }

        [HttpGet]
        //[OutputCache(Duration = 86400)]
        public async Task<ViewResult> MinhasDoacoes()
        {
            var userId = ObterUsuarioDaSecao().Id;

            var doacoes = await _usuarioBUS.ObterDoacoes(userId);

            return View(new MinhasDoacoesViewModel(doacoes));
        }

        [HttpGet]
        //[OutputCache(Duration = 86400)]
        public async Task<ViewResult> MeusInteresses()
        {

            var userId = ObterUsuarioDaSecao().Id;

            var interesses = await _usuarioBUS.ObterInteresses(userId);


            return View(new MeusInteressesViewModel(interesses));
        }
        #endregion

        #region [JsonResult]
        [HttpPost]
        [AllowAnonymous]
        [Route("Entrar")]
        public async Task<JsonResult> Entrar(RegistroEhLoginViewModel usuarioVM)
        {
            return await Task.Run(async () => Json(new
            {
                url = await LogIn(usuarioVM) ? "/Usuario/Home" : "/Site/Home"
            }));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Registrar")]
        public async Task<JsonResult> Registrar(RegistroEhLoginViewModel usuarioVM)
        {
            return await Task.Run(() => Json(_usuarioBUS.Registrar(new UsuarioMOD 
            {
                Email = usuarioVM.Email,
                Senha = usuarioVM.Senha
            })));
        }

        [HttpPost]
        public ActionResult CadastrarInteresse(InteresseViewModel interesseVM)
        {

            var id = ObterUsuarioDaSecao().Id;

            //_usuarioBUS.CadastrarInteresse(id, interesseVM.Interesse);

            TempData["msg"] = "Seu interesse foi cadastrado com sucesso!";

            return RedirectToAction("MeusInteresses");
        }

       


        #endregion

        #region [NonAction]
        
        [NonAction]
        private async Task<bool> LogIn(RegistroEhLoginViewModel usuarioVM)
        {
            Session.Timeout = 1440;

            var retornoUser = await _usuarioBUS.EhUsuario(new UsuarioMOD 
            {
                Email = usuarioVM.Email,
                Senha = usuarioVM.Senha
            });
            return (Session["napegada_auth"] = retornoUser) != null;
        }

        [NonAction]
        private void LogOut()
        {
            Session["napegada_auth"] = null;
        }        
        #endregion
    }
}