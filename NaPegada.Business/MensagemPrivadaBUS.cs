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
            var mensagemPrivadaDTO = await _usuarioREP.ObterMensagemPrivadaDTO(dto);

            var mensagem = new MensagemPrivadaMOD(mensagemPrivadaDTO);

            return mensagem;
        }
    }
}
