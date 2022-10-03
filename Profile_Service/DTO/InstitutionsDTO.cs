﻿using System.ComponentModel.DataAnnotations;

namespace Profile_Service.DTO
{
    public class InstitutionsDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;
    }
}