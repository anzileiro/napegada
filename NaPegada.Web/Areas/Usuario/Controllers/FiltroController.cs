using NaPegada.Business;
using NaPegada.Web.Models;
using System.Web.Mvc;
using System.Web.Security;
using NaPegada.Util;
using System;

namespace NaPegada.Web.Areas.User.Controllers
{
    public class AutenticarController : Controller, IInjecao<UsuarioBUS, Utilitaria>
    {
        private UsuarioBUS _usuarioBUS;
        private Utilitaria _utilitaria;

        public void Injetar(UsuarioBUS usuarioBUS_, Utilitaria utilitaria_)
        {
            this._usuarioBUS = usuarioBUS_;
            this._utilitaria = utilitaria_;
        }

        public AutenticarController()
        {
            this.Injetar(new UsuarioBUS(), new Utilitaria());
        }

        public bool Logar(UsuarioViewModel usuarioVM)
        {
            usuarioVM.Usuario.Senha = _utilitaria.CriptografarSenha(usuarioVM.Usuario.Senha, "sha1");
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

    public class Autorizar : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.Cookies["napegada_auth"] != null)
            {
                filterContext.RequestContext.HttpContext.Response.Cookies["napegada_auth"].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}
