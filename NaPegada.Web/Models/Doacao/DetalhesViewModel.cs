using NaPegada.Model;
using NaPegada.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaPegada.Web.Models.Doacao
{
    public class DetalhesViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Raca { get; set; }

        [Required]
        public AnimalEspecie? Especie { get; set; }

        [Required]
        public AnimalPorte? Porte { get; set; }
        public int Anos { get; set; }
        public int Meses { get; set; }

        public bool EhVacinado { get; set; }
        public bool EhCastrado { get; set; }
        public bool TomouVermifugo { get; set; }

        public SelectList Especies { get; set; }
        public SelectList Portes { get; set; }

        public DetalhesViewModel()
        {
            Especies = AnimalEspecie.Cachorro.ToSelectList();
            Portes = AnimalPorte.Grande.ToSelectList();
        }
    }
}