﻿using System.ComponentModel.DataAnnotations;

namespace OnlineStoreforElectronicComponents.Models.DTOs
{
    public class TypeOfComponentDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string TypeOfComponentName { get; set; }
    }
}
