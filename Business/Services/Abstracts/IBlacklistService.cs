using DTOs.Requests;
using DTOs.Responses;

namespace Business.Services.Abstracts;

public interface IBlacklistService
{
    Task<BlacklistResponse> CreateAsync(CreateBlacklistRequest request);
    Task<BlacklistResponse?> GetByIdAsync(int id);
    Task<List<BlacklistResponse>> GetAllAsync();
    Task DeleteAsync(int id);
} 