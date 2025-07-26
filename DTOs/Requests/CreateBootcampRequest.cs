using System.ComponentModel.DataAnnotations;

namespace DTOs.Requests;

public class CreateBootcampRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public int InstructorId { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
} 