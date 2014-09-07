using NaPegada.Business;
using NaPegada.Web.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace NaPegada.Web.Areas.User.Controllers
{
    public class AuthController : Controller
    {
        private readonly UsuarioBUS _usuarioBUS;
        private readonly Utility _utility;

        public AuthController(Utility utility_, UsuarioBUS usuarioBUS_)
        {
            _usuarioBUS = usuarioBUS_;
            _utility = utility_;
        }

        public bool Logar(UsuarioViewModel usuarioVM)
        {
            usuarioVM.Usuario.Senha = _utility.CriptografarSenha(usuarioVM.Usuario.Senha, "sha1");
            if (_usuarioBUS.EhUsuario(usuarioVM.Usuario))
            {
                FormsAuthentication.Authenticate(usuarioVM.Usuario.Email, usuarioVM.Usuario.Senha);
                FormsAuthentication.SetAuthCookie(usuarioVM.Usuario.Email, usuarioVM.Usuario.MantenhaMeConectado);
                return true;
            }
            return false;
        }

        public void Deslogar()
        {
            FormsAuthentication.SignOut();
        }

    }
}
