using System.ComponentModel.DataAnnotations;

namespace DTOs.Requests;

public class CreateInstructorRequest : CreateUserRequest
{
    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; } = string.Empty;
} 