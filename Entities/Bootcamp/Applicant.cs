using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Applicant : User
{
    [MaxLength(500)]
    public string? About { get; set; }
}