using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Profile_Service.Models
{
    public partial class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        public string LastName { get; set; } = String.Empty;

        [Required]
        public string Username { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        [Required]
        public string Role { get; set; } = "User";

        public string Institutions { get; set; } = String.Empty;

        public string Themes { get; set; } = String.Empty;

        public string ResidencePlace { get; set; } = String.Empty;
    }
}