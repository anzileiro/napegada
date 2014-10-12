using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaPegada.Web.Controllers
{
    public class BaseAsyncController : AsyncController
    {
        [NonAction]
        protected UsuarioMOD ObterUsuarioDaSecao()
        {
            return Session["napegada_auth"] as UsuarioMOD;
        }
    }
}