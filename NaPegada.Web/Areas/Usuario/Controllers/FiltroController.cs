using NaPegada.Business;
using NaPegada.Web.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace NaPegada.Web.Areas.User.Controllers
{
    public class AutenticarController : Controller
    {
        private readonly UsuarioBUS _usuarioBUS;
        private readonly Utilitaria _utilitaria;


        public AutenticarController()
        {
            _usuarioBUS = new UsuarioBUS();
            _utilitaria = new Utilitaria();
        }

        public bool Logar(UsuarioViewModel usuarioVM)
        {
            usuarioVM.Usuario.Senha = _utilitaria.CriptografarSenha(usuarioVM.Usuario.Senha, "sha1");
            if (_usuarioBUS.EhUsuario(usuarioVM.Usuario))
            {
                FormsAuthentication.Authenticate(usuarioVM.Usuario.Email, usuarioVM.Usuario.Senha);
                FormsAuthentication.SetAuthCookie(usuarioVM.Usuario.Email, false);
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
