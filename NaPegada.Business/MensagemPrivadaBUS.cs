using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Model.DTO.Doacao;
using NaPegada.Repository;
using NaPegada.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Business
{
    public class MensagemPrivadaBUS
    {
        private IUsuarioREP _usuarioREP;
        private IMensagemPrivadaREP _mensagemPrivadaREP;

        public MensagemPrivadaBUS()
        {
            _usuarioREP = new UsuarioREP();
            _mensagemPrivadaREP = new MensagemPrivadadaREP();
        }

        public async Task EnviarMensagemAdocao(AdocaoDTO dto)
        {
            var mensagem = await ObterMensagemPrivada(dto);

            await _mensagemPrivadaREP.Registrar(mensagem);
        }

        private async Task<MensagemPrivadaMOD> ObterMensagemPrivada(AdocaoDTO dto)
        {
            if (_mensagemPrivadaREP.JaEnviouSolicitacaoAdocao(dto))
                throw new InvalidOperationException("Você já enviou uma solicitação ao doador. Por favor, aguarde a resposta do doador");

            var mensagemPrivadaDTO = await _usuarioREP.ObterMensagemPrivadaDTO(dto);

            var mensagem = new MensagemPrivadaMOD(mensagemPrivadaDTO);

            return mensagem;
        }

        public async Task<IEnumerable<MensagemPrivadaMOD>> ObterMensagensRecebidas(ObjectId idUsuarioLogado)
        {
            var mensagensRecebidas = await _mensagemPrivadaREP.ObterMensagensRecebidas(idUsuarioLogado);

            return mensagensRecebidas;
        }

        public MensagemPrivadaMOD ObterPorId(string id)
        {
            var mongoId = ObjectId.Parse(id);
            var mensagem = _mensagemPrivadaREP.ObterPorId(mongoId);

            return mensagem;
        }
    }
}
