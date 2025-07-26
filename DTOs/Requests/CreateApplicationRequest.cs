using System.ComponentModel.DataAnnotations;

namespace DTOs.Requests;

public class CreateApplicationRequest
{
    [Required]
    public int ApplicantId { get; set; }
    
    [Required]
    public int BootcampId { get; set; }
} 