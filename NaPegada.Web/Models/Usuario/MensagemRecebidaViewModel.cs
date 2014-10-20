using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaPegada.Web.Models.Usuario
{
    public class MensagemRecebidaViewModel
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string Remetente { get; set; }
        public bool EhRequisicaoAdocao { get; set; }
        public string NomeAnimal { get; set; }
        public string IdDoacao { get; set; }

        public MensagemRecebidaViewModel(MensagemPrivadaMOD mensagem)
        {
            Id = mensagem.Id.ToString();
            Titulo = mensagem.Titulo;
            Conteudo = mensagem.Conteudo;
            Remetente = string.IsNullOrWhiteSpace(mensagem.Remetente.Nome) ?
                            mensagem.Remetente.Email :
                            string.Format("{0} - {1}", mensagem.Remetente.Nome, mensagem.Remetente.Email);
            EhRequisicaoAdocao = mensagem.EhRequisicaoAdocao();

            if(EhRequisicaoAdocao)
            {
                NomeAnimal = mensagem.Doacao.NomeAnimal;
                IdDoacao = mensagem.Doacao.IdDoacao.ToString();
            }
        }
    }
}