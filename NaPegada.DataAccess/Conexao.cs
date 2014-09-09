using MongoDB.Driver;
using NaPegada.Util;
using System;

namespace NaPegada.DataAccess
{
    public class Conexao<T> : IInjecao<MongoClient>, IDisposable
    {
        private MongoClient _cliente;

        public void Injetar(MongoClient cliente_)
        {
            this._cliente = cliente_;
        }

        public Conexao()
        {
            this.Injetar(new MongoClient());
        }

        public MongoCollection<T> Conectar(string uri, string banco, string colecao)
        {
            _cliente = new MongoClient(uri);
            var _servidor = _cliente.GetServer();
            var _banco = _servidor.GetDatabase(banco);

            return _banco.GetCollection<T>(colecao);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
