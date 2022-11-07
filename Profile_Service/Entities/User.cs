using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Profile_Service.Entities
{
    public partial class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime EnrollmentDate { get; set; }

        public string Role { get; set; } = "User";

        public string Institutions { get; set; } = null!;

        public string Themes { get; set; } = null!;

        public string ResidencePlace { get; set; } = null!;
    }
}