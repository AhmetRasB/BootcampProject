using Entities.Enums;

namespace DTOs.Responses;

public class ApplicationResponse
{
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    public string ApplicantName { get; set; } = string.Empty;
    public int BootcampId { get; set; }
    public string BootcampName { get; set; } = string.Empty;
    public ApplicationState ApplicationState { get; set; }
    public DateTime CreatedDate { get; set; }
} 