using DTOs.Requests;
using DTOs.Responses;

namespace Business.Services.Abstracts;

public interface IBootcampService
{
    Task<BootcampResponse> CreateAsync(CreateBootcampRequest request);
    Task<BootcampResponse?> GetByIdAsync(int id);
    Task<List<BootcampResponse>> GetAllAsync();
    Task<BootcampResponse> UpdateAsync(int id, CreateBootcampRequest request);
    Task DeleteAsync(int id);
} 