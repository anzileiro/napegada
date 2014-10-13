using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Usuario
{
    public class MeusInteressesViewModel
    {

        public IEnumerable<InteresseViewModel> Interesses { get; set; }

        public MeusInteressesViewModel(IEnumerable<InteresseMOD> interesses)
        {
            var models = new List<InteresseViewModel>();


            foreach (var interesse in interesses)
                models.Add(new InteresseViewModel(interesse));

            Interesses = models;
        }
    }
}