using System;
using System.Web;
using System.Web.Mvc;

namespace NaPegada.Web.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
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
                contexto.Result = new RedirectResult("/Site/Home");
            }
        }
    }
}