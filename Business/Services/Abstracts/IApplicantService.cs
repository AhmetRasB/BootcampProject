using DTOs.Requests;
using DTOs.Responses;

namespace Business.Services.Abstracts;

public interface IApplicantService
{
    Task<ApplicantResponse> CreateAsync(CreateApplicantRequest request);
    Task<ApplicantResponse?> GetByIdAsync(int id);
    Task<List<ApplicantResponse>> GetAllAsync();
    Task<ApplicantResponse> UpdateAsync(int id, CreateApplicantRequest request);
    Task DeleteAsync(int id);
} 