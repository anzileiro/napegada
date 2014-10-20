using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Usuario
{
    public class ResumoMensagemRecebidaViewModel
    {
        public string IdMensagem { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }

        public ResumoMensagemRecebidaViewModel(MensagemPrivadaMOD mensagem)
        {
            IdMensagem = mensagem.Id.ToString();
            Titulo = mensagem.EhRequisicaoAdocao() ? string.Format("Solicitação de adoção de {0}", mensagem.Doacao.NomeAnimal) : mensagem.Titulo;            
            Subtitulo = string.IsNullOrWhiteSpace(mensagem.Remetente.Nome) ?
                            mensagem.Remetente.Email : 
                            string.Format("{0} - {1}", mensagem.Remetente.Nome, mensagem.Remetente.Email);
        }
    }
}