using NaPegada.Model;
using NaPegada.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Tests.Stubs
{
    public class MensagemPrivadaREPStub : IMensagemPrivadaREP
    {
        private IList<MensagemPrivadaMOD> _mensagens = new List<MensagemPrivadaMOD>();

        public async Task Registrar(Model.MensagemPrivadaMOD mensagem)
        {
            await Task.Run(() => _mensagens.Add(mensagem)); 
        }

        public async Task<IEnumerable<Model.MensagemPrivadaMOD>> ObterMensagensRecebidas(MongoDB.Bson.ObjectId idUsuarioLogado)
        {
            return await Task.Run(() => _mensagens.Where(_ => _.Destinatario.IdUsuario == idUsuarioLogado));
        }

        public Model.MensagemPrivadaMOD ObterPorId(MongoDB.Bson.ObjectId id)
        {
            return _mensagens.Single(_ => _.Id == id);
        }

        public bool JaEnviouSolicitacaoAdocao(Model.DTO.Doacao.AdocaoDTO dto)
        {
            return _mensagens.Any(_ => _.Remetente.IdUsuario == dto.Adotante.Id && _.Doacao.IdDoacao == dto.IdDoacao);
        }
    }
}
