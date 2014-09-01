using MongoDB.Bson;

namespace NaPegada.Model
{
    public abstract class ObjectMongo
    {
        public ObjectId Id { get; set; }
    }
}
