using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Entities;

public class Bootcamp: BaseEntity
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
    [Required] 
    public BootcampState BootcampState { get; set; } = BootcampState.PREPARING;
    
    [ForeignKey("InstructorId")]
    public virtual Instructor Instructor { get; set; } = null!;
}