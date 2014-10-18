using NaPegada.Model;
using NaPegada.Web.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Site
{
    public class HomeViewModel
    {

        public IEnumerable<DoacaoViewModel> TodasDoacoes { get; set; }

        public HomeViewModel(IEnumerable<DoacaoMOD> doacoes)
        {
            var models = new List<DoacaoViewModel>();

            foreach (var doacao in doacoes)
                models.Add(new DoacaoViewModel(doacao));

            TodasDoacoes = models;
        }


    }
}