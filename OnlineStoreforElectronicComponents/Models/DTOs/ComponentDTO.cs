﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreforElectronicComponents.Models.DTOs;
public class ComponentDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string? ComponentName { get; set; }

    [Required]
    [MaxLength(40)]
    public string? PackageName { get; set; }
    [Required]
    public double Price { get; set; }
    public string? Image { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public IFormFile? ImageFile { get; set; }
    public IEnumerable<SelectListItem>? CategoryList { get; set; }
}
