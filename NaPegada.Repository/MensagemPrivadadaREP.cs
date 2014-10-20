using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NaPegada.DataAccess;
using NaPegada.Model;
using NaPegada.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Repository
{
    public class MensagemPrivadadaREP : IMensagemPrivadaREP
    {
        private readonly MongoCollection _mensagens;

        public MensagemPrivadadaREP()
        {
            var conexao = new Conexao<MensageiroMOD>();
            _mensagens = conexao.Conectar("mongodb://localhost", "napegada", "mensagemPrivada");
        }

        public async Task Registrar(MensagemPrivadaMOD mensagem)
        {
            await Task.Run(() => _mensagens.Insert(mensagem));
        }

        public async Task<IEnumerable<MensagemPrivadaMOD>> ObterMensagensRecebidas(ObjectId idUsuarioLogado)
        {
            return await Task.Run(() => _mensagens.FindAs<MensagemPrivadaMOD>(Query<MensagemPrivadaMOD>.EQ(_ => _.Destinatario.IdUsuario, idUsuarioLogado)).ToList());
        }

        public MensagemPrivadaMOD ObterPorId(ObjectId id)
        {
            return _mensagens.FindAs<MensagemPrivadaMOD>(Query<MensagemPrivadaMOD>.EQ(_ => _.Id, id)).Single();
        }
    }
}
