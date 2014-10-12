using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Doacao
{
    public class ExclusaoViewModel
    {
        public string Id { get; set; }
        public string NomeAnimal { get; set; }

        public ExclusaoViewModel(DoacaoMOD doacao)
        {
            Id = doacao.Id.ToString();
            NomeAnimal = doacao.NomeAnimal;
        }
    }
}