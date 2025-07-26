using System.ComponentModel.DataAnnotations;

namespace DTOs.Requests;

public class CreateApplicantRequest : CreateUserRequest
{
    [MaxLength(500)]
    public string? About { get; set; }
} 