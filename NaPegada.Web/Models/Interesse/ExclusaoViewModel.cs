using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Interesse
{
    public class ExclusaoViewModel
    {
        public string Id { get; set; }
        public string Raca { get; set; }

        public ExclusaoViewModel(InteresseMOD interesse)
        {
            Id = interesse.Id.ToString();
            Raca = interesse.Raca;
        }



    }
}