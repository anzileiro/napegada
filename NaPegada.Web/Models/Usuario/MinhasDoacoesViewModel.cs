using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Usuario
{
    public class MinhasDoacoesViewModel
    {
        public IEnumerable<DoacaoViewModel> Doacoes { get; set; }

        public MinhasDoacoesViewModel(IEnumerable<DoacaoMOD> doacoes)
        {
            var models = new List<DoacaoViewModel>();

            foreach (var doacao in doacoes)
                models.Add(new DoacaoViewModel(doacao));

            Doacoes = models;
        }
    }
}