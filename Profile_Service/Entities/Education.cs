using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Profile_Service.Entities
{
    public class Education
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        public string? EducationId { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }
    }
}
