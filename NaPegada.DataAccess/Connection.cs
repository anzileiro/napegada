using MongoDB.Driver;

namespace NaPegada.DataAccess
{
    public class Connection<T>
    {
        public MongoCollection<T> Connect(string uri, string database, string collection)
        {
            var mongoClient = new MongoClient(uri);
            var mongoServer = mongoClient.GetServer();
            var mongoDatabase = mongoServer.GetDatabase(database);

            return mongoDatabase.GetCollection<T>(collection);
        }
    }
}
