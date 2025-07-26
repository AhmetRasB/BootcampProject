using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Employee : User
{
    [Required]
    [MaxLength(100)]
    public string Position { get; set; } = string.Empty;
}