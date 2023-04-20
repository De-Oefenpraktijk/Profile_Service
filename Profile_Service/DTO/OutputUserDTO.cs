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

<<<<<<< HEAD:Profile_Service/DTO/OutputUserDTO.cs
        public string? Workplace { get; set; }
=======
        public string? ResidencePlace { get; set; }
        public string? Biography { get; set; }
>>>>>>> OP-82-modify-professional-information:Profile_Service/DTO/UserDTO.cs
    }
}
