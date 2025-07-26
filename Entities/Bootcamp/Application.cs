using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Entities;

public class Application : BaseEntity
{
    [Required]
    public int ApplicantId { get; set; }
    [Required]
    public int BootcampId { get; set; }
    [Required] 
    public ApplicationState ApplicationState { get; set; } = ApplicationState.PENDING;
    
    [ForeignKey("ApplicantId")]
    public virtual Applicant Applicant { get; set; } = null!;
    [ForeignKey("BootcampId")]
    public virtual Bootcamp Bootcamp { get; set; } = null!;
}