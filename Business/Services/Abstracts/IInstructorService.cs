using DTOs.Requests;
using DTOs.Responses;

namespace Business.Services.Abstracts;

public interface IInstructorService
{
    Task<InstructorResponse> CreateAsync(CreateInstructorRequest request);
    Task<InstructorResponse?> GetByIdAsync(int id);
    Task<List<InstructorResponse>> GetAllAsync();
    Task<InstructorResponse> UpdateAsync(int id, CreateInstructorRequest request);
    Task DeleteAsync(int id);
} 