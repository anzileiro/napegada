using MongoDB.Bson;

namespace NaPegada.Model
{
    public abstract class Object
    {
        public ObjectId Id { get; set; }
    }
}
