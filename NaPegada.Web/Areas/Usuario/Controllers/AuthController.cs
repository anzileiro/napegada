using NaPegada.Business;
using NaPegada.Web.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace NaPegada.Web.Areas.User.Controllers
{
    public abstract class AuthController : Controller
    {
        private readonly UsuarioBUS _userBUS;

        public AuthController()
        {
            _userBUS = new UsuarioBUS();
        }

        public void SignIn(UsuarioViewModel usuarioVM)
        {
            if (_userBUS.EhUsuario(usuarioVM.Usuario))
            {
                FormsAuthentication.Authenticate(usuarioVM.Usuario.Email, usuarioVM.Usuario.Senha);
                FormsAuthentication.SetAuthCookie(usuarioVM.Usuario.Email, usuarioVM.Usuario.MantenhaMeConectado);
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

    }
}
