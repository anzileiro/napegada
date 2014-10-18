using MongoDB.Driver;
using System;

namespace NaPegada.DataAccess
{
    public class Conexao<T>
    {
        public MongoCollection<T> Conectar(string uri, string db, string colecao)
        {
            var cliente = new MongoClient(uri);
            var servidor = cliente.GetServer();
            var banco = servidor.GetDatabase(db);

            return banco.GetCollection<T>(colecao);
        }
    }
}
