using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Profile_Service.Entities
{
    public class Education
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }
    }
}
