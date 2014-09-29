using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NaPegada.Web.Controllers
{
    public class Autenticar : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase contextoHttp)
        {
            return HttpContext.Current.Session["napegada_auth"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext contexto)
        {
            if (contexto.HttpContext.Request.IsAjaxRequest())
            {
                contexto.Result = new JavaScriptResult() { Script = "top.location.reload()" };
            }
            else
            {
                base.HandleUnauthorizedRequest(contexto);
                contexto.Controller.TempData["Mensagem"] = "Por favor faça login!";
                contexto.Result = new RedirectResult("/Usuario/Entrar");
            }
        }
    }
}