using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Blacklist : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Reason { get; set; } = string.Empty;
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    [Required] 
    public int ApplicantId { get; set; }
    
    [ForeignKey("ApplicantId")]
    public virtual Applicant Applicant { get; set; } = null!;
}