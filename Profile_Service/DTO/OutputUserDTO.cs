using MongoDB.Driver;
using Profile_Service.Entities;

namespace Profile_Service.DTO
{
    public class OutputUserDTO
    {
        public string? Id { get; set; }
        
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Role { get; set; }

        public List<string>? Educations { get; set; }

        public List<string>? Specializations { get; set; }
        public string? Workplace { get; set; }
        public string? Biography { get; set; }
    }
}
