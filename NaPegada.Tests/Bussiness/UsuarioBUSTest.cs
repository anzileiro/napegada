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

        public UsuarioBUSTest()
        {
            _usuarioLogado = new UsuarioMOD();
            _usuarioLogado.Id = ObjectId.GenerateNewId();
            _userREP = new UsuarioREPStub();
            _userREP.Registrar(_usuarioLogado);
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
    }
}
