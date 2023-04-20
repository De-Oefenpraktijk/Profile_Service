using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Profile_Service.Entities
{
    public partial class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Role { get; set; }

        public List<string>? Educations { get; set; }
        
        public List<string>? Specializations { get; set; }

<<<<<<< HEAD
        public string? Workplace { get; set; }
=======
        public string? ResidencePlace { get; set; }
        public string? Biography { get; set; }
>>>>>>> OP-82-modify-professional-information
    }
}