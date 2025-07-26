using Entities.Enums;

namespace DTOs.Responses;

public class BootcampResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int InstructorId { get; set; }
    public string InstructorName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public BootcampState BootcampState { get; set; }
    public DateTime CreatedDate { get; set; }
} 