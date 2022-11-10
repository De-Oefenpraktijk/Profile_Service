using System.ComponentModel.DataAnnotations;
using Profile_Service.Entities;

namespace Profile_Service.DTO
{
    public class SpecializationDTO
    {
        public SpecializationDTO(Specialization theme)
        {
            Id = theme.Id!;
            Name = theme.Name;
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
