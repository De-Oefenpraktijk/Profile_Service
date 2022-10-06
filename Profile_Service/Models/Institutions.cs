using System.ComponentModel.DataAnnotations;

namespace Profile_Service.Models
{
    public class Institutions
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;
    }
}
