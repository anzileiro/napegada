using MongoDB.Bson;
using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Interesse
{
    public class DetalhesViewModel
    {

        public string Id { get; set; }
        public string Raca { get; set; }

        [Required(ErrorMessage = "Espécie é obrigatória")]
        public AnimalEspecie Especie { get; set; }

        [Range(0,10, ErrorMessage="Idade mínima inválida: 0 ~ 10")]
        public ushort? IdadeMin { get; set; }

        [Range(0, 10, ErrorMessage = "Idade máxima inválida: 0 ~ 10")]
        public ushort? IdadeMax { get; set; }
        public bool PortePequeno { get; set; }
        public bool PorteMedio { get; set; }
        public bool PorteGrande { get; set; }
        public bool Vacinado { get; set; }
        public bool Castrado { get; set; }
        public bool Vermifugo { get; set; }


        public DetalhesViewModel()
        {

        }


        public DetalhesViewModel(InteresseMOD interesse)
        {
            
            Id = interesse.Id.ToString();
            Raca = interesse.Raca;
            Especie = interesse.Especie;
            PortePequeno = interesse.Porte.Contains(AnimalPorte.Pequeno) ? true : false;
            PorteMedio = interesse.Porte.Contains(AnimalPorte.Médio) ? true : false;
            PorteGrande = interesse.Porte.Contains(AnimalPorte.Grande) ? true : false;
            Vacinado = interesse.EhVacinado;
            Castrado = interesse.EhCastrado;
            Vermifugo = interesse.TomouVermifugo;
            IdadeMin = interesse.IdadeMinimaEmAnos;
            IdadeMax = interesse.IdadeMaximaEmAnos;

        }

        public InteresseMOD ConverterParaInteresse()
        {

            var interesse = new InteresseMOD();

            interesse.Id = string.IsNullOrWhiteSpace(Id) ? ObjectId.GenerateNewId() : ObjectId.Parse(Id);
            interesse.Raca = Raca;
            interesse.Especie = Especie;
            interesse.EhVacinado = Vacinado;
            interesse.EhCastrado = Castrado;
            interesse.TomouVermifugo = Vermifugo;

            interesse.Porte = AdicionarPortes();

            interesse.IdadeMinimaEmAnos = IdadeMin;
            interesse.IdadeMaximaEmAnos = IdadeMax;


            return interesse;
        }



        private List<AnimalPorte> AdicionarPortes()
        {
            List<AnimalPorte> listaPortes = new List<AnimalPorte>();

            if (PortePequeno == true)
            {
                listaPortes.Add(AnimalPorte.Pequeno);
            }

            if (PorteMedio == true)
            {
                listaPortes.Add(AnimalPorte.Médio);
            }

            if (PorteGrande == true)
            {
                listaPortes.Add(AnimalPorte.Grande);
            }


            return listaPortes.ToList();
        }



    }
}