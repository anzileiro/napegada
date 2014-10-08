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
        public bool PortePequeno { get; set; }
        public bool PorteMedio { get; set; }
        public bool PorteGrande { get; set; }
        public bool Vacinado { get; set; }
        public bool Castrado { get; set; }
        public bool Vermifugo { get; set; }


        public InteresseViewModel(InteresseMOD interesse)
        {
            Id = interesse.Id.ToString();
            Especie = interesse.Especie.ToString();
            Raca = interesse.Raca;
            IdadeMin = interesse.IdadeMinimaEmAnos.ToString();
            IdadeMax = interesse.IdadeMaximaEmAnos.ToString();
           

            //interesse.Porte = adicionaPortes();
        }


        private List<String> adicionaPortes()
        {

            List<string> listaPorte = new List<string>();

            if(PortePequeno == true){
                listaPorte.Add("Pequeno");     
            }

            if(PorteMedio == true){
                listaPorte.Add("Médio");     
            }
            
            if(PorteGrande == true){
                listaPorte.Add("Grande");     
            }


            return listaPorte.ToList();
        }

    }
}