using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Usuario
{
    public class DoacaoViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public string Especie { get; set; }
        public string DataCadastro { get; set; }
        public string Idade { get; set; }
        public string Vacinado { get; set; }
        public string Castrado { get; set; }
        public string Vermifugo { get; set; }
        public string Porte { get; set; }

        public DoacaoViewModel(DoacaoMOD doacao)
        {
            Id = doacao.Id.ToString();
            Nome = doacao.NomeAnimal;
            Raca = doacao.RacaAnimal;
            Especie = doacao.EspecieAnimal.ToString();
            DataCadastro = doacao.DataCadastro.ToShortDateString();
            Idade = doacao.IdadeAnimal.ToString();
            Vacinado = doacao.EhVacinado ? "Sim" : "Não";
            Castrado = doacao.EhCastrado ? "Sim" : "Não";
            Vermifugo = doacao.TomouVermifugo ? "Sim" : "Não";
            Porte = doacao.PorteAnimal.ToString();
        }
    }
}