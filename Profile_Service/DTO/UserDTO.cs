using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Profile_Service.Entities;

namespace Profile_Service.DTO
{
    public class UserDTO
    {
        public UserDTO(User user)
        {
            Id = user.Id!;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            EmailAddress = user.EmailAddress;
            Password = user.Password;
            EnrollmentDate = user.EnrollmentDate;
            Role = user.Role;
            Institutions = user.Institutions;
            Themes = user.Themes;
            ResidencePlace = user.ResidencePlace;
        }

        [Required]
        public string Id { get; set; } = null!;

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
