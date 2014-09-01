using NaPegada.Business;
using NaPegada.Web.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace NaPegada.Web.Areas.User.Controllers
{
    public abstract class AuthController : Controller
    {
        private readonly UsuarioBUS _usuarioBUS;
        private readonly Utility _utility;

        public AuthController()
        {
            _usuarioBUS = new UsuarioBUS();
            _utility = new Utility();
        }

        public bool Logar(UsuarioViewModel usuarioVM)
        {
            var existeUsuario = false;
            usuarioVM.Usuario.Senha = _utility.CriptografarSenha(usuarioVM.Usuario.Senha, "sha1");
            if (_usuarioBUS.EhUsuario(usuarioVM.Usuario))
            {
                FormsAuthentication.Authenticate(usuarioVM.Usuario.Email, usuarioVM.Usuario.Senha);
                FormsAuthentication.SetAuthCookie(usuarioVM.Usuario.Email, usuarioVM.Usuario.MantenhaMeConectado);
                existeUsuario = true;
            }
            return existeUsuario;
        }

        public void Deslogar()
        {
            FormsAuthentication.SignOut();
        }

    }
}
