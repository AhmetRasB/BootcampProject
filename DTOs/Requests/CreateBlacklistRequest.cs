using System.ComponentModel.DataAnnotations;

namespace DTOs.Requests;

public class CreateBlacklistRequest
{
    [Required]
    [MaxLength(200)]
    public string Reason { get; set; } = string.Empty;
    
    [Required]
    public int ApplicantId { get; set; }
} 