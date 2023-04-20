using MongoDB.Driver;
using Profile_Service.Entities;

namespace Profile_Service.DTO
{
    public class UserDTO
    {
        public string? Id { get; set; }
        
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Username { get; set; }

        public string? EmailAddress { get; set; }

        public string? Password { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public string? Role { get; set; }

        public List<string>? Educations { get; set; }

        public List<string>? Specializations { get; set; }

        public string? ResidencePlace { get; set; }
        public string? Biography { get; set; }
    }
}
