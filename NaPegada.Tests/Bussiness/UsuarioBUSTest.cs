using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Model.DTO;
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
    public class UsuarioBUSTest
    {
        private readonly UsuarioMOD _usuarioLogado;
        private readonly IUsuarioREP _userREP;
        private readonly DoacaoMOD _doacaoDefault;

        public UsuarioBUSTest()
        {
            _usuarioLogado = new UsuarioMOD();
            _usuarioLogado.Id = ObjectId.GenerateNewId();
            _doacaoDefault = ObterDoacaoDefault();
            _usuarioLogado.AdicionarDoacao(_doacaoDefault);
            _userREP = new UsuarioREPStub();
            _userREP.Registrar(_usuarioLogado);
        }

        private DoacaoMOD ObterDoacaoDefault()
        {
            var doacao = new DoacaoMOD();

            doacao.Id = ObjectId.GenerateNewId();
            doacao.NomeAnimal = "Rex";

            return doacao;
        }

        [TestMethod]
        public async Task DeveRegistrarDoacao()
        {
            var dto = ObterRegistroDoacaoDTO();

            await _userREP.RegistrarDoacao(dto);

            var doacao = _userREP.ObterDoacao(dto.Doacao.Id);
            Assert.IsNotNull(doacao);
        }

        private RegistroDoacaoDTO ObterRegistroDoacaoDTO()
        {
            var doacao = new DoacaoMOD();
            var id = ObjectId.GenerateNewId();            

            doacao.Id = id;
            doacao.NomeAnimal = "Totó";

            var dto = new RegistroDoacaoDTO();
            dto.Doacao = doacao;
            dto.IdUsuario = _usuarioLogado.Id;

            return dto;
        }

        [TestMethod]
        public async Task DeveAtualizarDoacao()
        {
            var dto = ObterRegistroDoacaoDTOAtualizacao();

            await _userREP.AtualizarDoacao(dto);

            var doacao = await _userREP.ObterDoacao(_doacaoDefault.Id);
            Assert.AreEqual("Totó", doacao.NomeAnimal);
        }

        private RegistroDoacaoDTO ObterRegistroDoacaoDTOAtualizacao()
        {
            var doacao = new DoacaoMOD();

            doacao.Id = _doacaoDefault.Id;
            doacao.NomeAnimal = "Totó";

            var dto = new RegistroDoacaoDTO();
            dto.Doacao = doacao;
            dto.IdUsuario = _usuarioLogado.Id;

            return dto;
        }

        [TestMethod]
        public async Task DeveObterTodasAsDoacoesCadastradas()
        {
            var doacoes = await _userREP.ObterDoacoes(_usuarioLogado.Id);

            Assert.AreEqual(1, doacoes.Count());
            Assert.IsTrue(doacoes.Any(_ => _.Id == _doacaoDefault.Id));
        }

        [TestMethod]
        public async Task DeveExcluirDoacao()
        {
            var dto = ObterExclusaoDoacaoDTO();

            await _userREP.ExcluirDoacao(dto);
            var doacao = await _userREP.ObterDoacao(_doacaoDefault.Id);

            Assert.IsNull(doacao);
        }

        private ExclusaoDoacaoDTO ObterExclusaoDoacaoDTO()
        {
            var dto = new ExclusaoDoacaoDTO();

            dto.IdDoacao = _doacaoDefault.Id;
            dto.IdUsuario = _usuarioLogado.Id;

            return dto;
        }
    }
}
