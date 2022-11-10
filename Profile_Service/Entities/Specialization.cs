using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Profile_Service.Entities
{
    public class Specialization
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
