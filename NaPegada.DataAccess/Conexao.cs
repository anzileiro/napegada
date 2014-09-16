using MongoDB.Driver;
using System;

namespace NaPegada.DataAccess
{
    public class Conexao<T> : IDisposable
    {
        public MongoCollection<T> Conectar(string uri, string banco, string colecao)
        {
            var _cliente = new MongoClient(uri);
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
