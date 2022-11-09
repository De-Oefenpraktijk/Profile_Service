using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Profile_Service.Entities
{
    public partial class User
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        
        public string UserId { get; set; }

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