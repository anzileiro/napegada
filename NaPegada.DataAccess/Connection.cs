using MongoDB.Driver;

namespace NaPegada.DataAccess
{
    public class Connection<T>
    {
        private MongoCollection<T> Collection { get; set; }

        public void Connect(string uri, string database, string collection)
        {
            var mongoClient = new MongoClient(uri);
            var mongoServer = mongoClient.GetServer();
            var mongoDatabase = mongoServer.GetDatabase(database);
            Collection = mongoDatabase.GetCollection<T>(collection);
        }

        public MongoCollection<T> GetCollection()
        {
            return Collection;
        }
    }
}
