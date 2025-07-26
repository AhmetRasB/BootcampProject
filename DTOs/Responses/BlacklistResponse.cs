namespace DTOs.Responses;

public class BlacklistResponse
{
    public int Id { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int ApplicantId { get; set; }
    public string ApplicantName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
} 