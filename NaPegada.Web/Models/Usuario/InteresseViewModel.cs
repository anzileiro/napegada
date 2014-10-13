using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Usuario
{
    public class InteresseViewModel
    {

        public string Id { get; set; }
        public string Raca { get; set; }
        public string Especie { get; set; }
        public string IdadeMin { get; set; }
        public string IdadeMax { get; set; }
        public string PortePequeno { get; set; }
        public string PorteMedio { get; set; }
        public string PorteGrande { get; set; }
        public string Vacinado { get; set; }
        public string Castrado { get; set; }
        public string Vermifugo { get; set; }


        public InteresseViewModel(InteresseMOD interesse)
        {
            Id = interesse.Id.ToString();
            Especie = interesse.Especie.ToString();
            Raca = interesse.Raca;
            IdadeMin = interesse.IdadeMinimaEmAnos.ToString();
            IdadeMax = interesse.IdadeMaximaEmAnos.ToString();
            PortePequeno = interesse.Porte.Contains(AnimalPorte.Pequeno) ? "Sim" : "Não";
            PorteMedio = interesse.Porte.Contains(AnimalPorte.Médio) ? "Sim" : "Não";
            PorteGrande = interesse.Porte.Contains(AnimalPorte.Grande) ? "Sim" : "Não";
            Vacinado = interesse.EhVacinado ? "Sim" : "Não";
            Castrado = interesse.EhCastrado ? "Sim" : "Não";
            Vermifugo = interesse.TomouVermifugo ? "Sim" : "Não";


            //interesse.Porte = adicionaPortes();
        }

    }
}