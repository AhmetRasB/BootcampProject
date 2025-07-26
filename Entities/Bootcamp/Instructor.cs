using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Instructor : User
{
    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; } = string.Empty;
}