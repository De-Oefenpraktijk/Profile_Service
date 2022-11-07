using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Profile_Service.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Profile_Service.DTO
{
    public class UserDTO
    {
        


        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string EmailAddress { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        public string Role { get; set; } = "User";

        public string? Institutions { get; set; }

        public string? Themes { get; set; }

        public string? ResidencePlace { get; set; }
    }
}
