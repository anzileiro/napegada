using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Usuario
{
    public class MensagensRecebidasViewModel
    {
        public IEnumerable<ResumoMensagemRecebidaViewModel> Mensagens { get; set; }

        public MensagensRecebidasViewModel(IEnumerable<MensagemPrivadaMOD> mensagens)
        {
            var viewModels = new List<ResumoMensagemRecebidaViewModel>();

            foreach (var mensagem in mensagens)
                viewModels.Add(new ResumoMensagemRecebidaViewModel(mensagem));

            Mensagens = viewModels;
        }
    }
}