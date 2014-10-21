using NaPegada.Business;
using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NaPegada.Web.Controllers
{
    //[AutenticarAutorizar]
    public class BaseAsyncController : AsyncController
    {
        [NonAction]
        protected UsuarioMOD ObterUsuarioDaSecao()
        {
            return Session["napegada_auth"] as UsuarioMOD;
        }

        [HttpGet]
        public async Task<JsonResult> Racas(AnimalEspecie especie)
        {
            var racaBus = new RacaBUS();

            var racas = await racaBus.BuscarPorEspecie(especie);

            return Json(new { Racas = racas }, JsonRequestBehavior.AllowGet);
        }
    }
}