using NaPegada.Web.Models.Doacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaPegada.Web.Controllers
{
    [AutenticarAutorizar]
    public class DoacaoController : AsyncController
    {        
        [HttpGet]
        public PartialViewResult Detalhes(string id = null)
        {
            return PartialView("_Doacao", new DetalhesViewModel());
        }
	}
}