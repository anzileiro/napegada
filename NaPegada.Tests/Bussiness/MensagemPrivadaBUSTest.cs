using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using NaPegada.Business;
using NaPegada.Model;
using NaPegada.Model.DTO.Doacao;
using NaPegada.Repository.Interfaces;
using NaPegada.Tests.Stubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Tests.Bussiness
{
    [TestClass]
    public class MensagemPrivadaBUSTest
    {
        private readonly UsuarioMOD _doador = new UsuarioMOD
                                                {
                                                    Id = ObjectId.GenerateNewId()
                                                };
        private readonly MensagemPrivadaBUS _mensagemPrivadaBUS;
        private readonly UsuarioMOD _adotante = new UsuarioMOD
                                                {
                                                    Id = ObjectId.GenerateNewId()
                                                };
        private readonly DoacaoMOD _doacaoDefault = new DoacaoMOD
                                                    {
                                                        Id = ObjectId.GenerateNewId()
                                                    };
        private readonly IMensagemPrivadaREP _mensagemPrivadaREP;

        public MensagemPrivadaBUSTest()
        {
            _mensagemPrivadaREP = new MensagemPrivadaREPStub();
            IUsuarioREP usuarioREP = ObterUsuarioREP().Result;
            
            _mensagemPrivadaBUS = new MensagemPrivadaBUS(_mensagemPrivadaREP, usuarioREP);           
        }

        private async Task<IUsuarioREP> ObterUsuarioREP()
        {
            IUsuarioREP usuarioREP = new UsuarioREPStub();

            await usuarioREP.Registrar(_doador);
            await usuarioREP.Registrar(_adotante);
            await usuarioREP.RegistrarDoacao(new RegistroDoacaoDTO
            {
                Doacao = _doacaoDefault,
                IdUsuario = _doador.Id
            });

            return usuarioREP;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SeEnviarMaisDeUmaSolicitacaoDeAdocaoDeveLancarExcecao()
        {
            var dto = new AdocaoDTO(_doacaoDefault.Id.ToString(), _adotante);

            await _mensagemPrivadaBUS.EnviarMensagemAdocao(dto);
            await _mensagemPrivadaBUS.EnviarMensagemAdocao(dto);
        }

        [TestMethod]
        public async Task DeveObterMensagem()
        {
            var id = ObjectId.GenerateNewId();

            var mensagem = new MensagemPrivadaMOD
            {
                Id = id
            };

            await _mensagemPrivadaREP.Registrar(mensagem);

            Assert.IsNotNull(_mensagemPrivadaBUS.ObterPorId(id.ToString()));
        }

        [TestMethod]
        public async Task DeveObterMensagensRecebidas()
        {
            var mensagem = new MensagemPrivadaMOD
            {
                Destinatario = new MensageiroMOD
                {
                    IdUsuario = _doador.Id
                }
            };

            await _mensagemPrivadaREP.Registrar(mensagem);

            var mensagens = await _mensagemPrivadaBUS.ObterMensagensRecebidas(_doador.Id);

            Assert.AreEqual(1, mensagens.Count());
        }
    }
}
