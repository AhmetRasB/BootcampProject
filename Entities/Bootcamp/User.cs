using System.ComponentModel.DataAnnotations;

namespace Entities;

public class User: BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    [MaxLength(11)] // TC ye gore
    public string NationalityIdentity { get; set; } = string.Empty;
    [Required] 
    public string Email { get; set; } = string.Empty;
    [Required]
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    [Required]
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
}