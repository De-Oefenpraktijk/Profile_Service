using System.ComponentModel.DataAnnotations;
using Profile_Service.Entities;

namespace Profile_Service.DTO
{
    public class InstitutionsDTO
    {
        public InstitutionsDTO(Institutions institution)
        {
            Id = institution.Id!;
            Name = institution.Name;
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
