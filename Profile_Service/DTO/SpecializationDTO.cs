using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Profile_Service.Entities;

namespace Profile_Service.DTO
{
    public class SpecializationDTO
    {
        [Required]
        public ObjectId Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
