using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Usuario
{
    public class MinhasDoacoesViewModel
    {
        public IEnumerable<DoacaoViewModel> Doacoes { get; set; }

        public MinhasDoacoesViewModel()
        {
            Doacoes = new List<DoacaoViewModel>();
        }
    }
}