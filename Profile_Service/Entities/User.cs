using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

        public string? EmailAddress { get; set; }

        public string? Password { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public string? Role { get; set; }

        public string? Institutions { get; set; }

        public string? Themes { get; set; }

        public string? ResidencePlace { get; set; }
    }
}