using DTOs.Requests;
using DTOs.Responses;

namespace Business.Services.Abstracts;

public interface IUserService
{
    Task<UserResponse> CreateAsync(CreateUserRequest request);
    Task<UserResponse?> GetByIdAsync(int id);
    Task<List<UserResponse>> GetAllAsync();
    Task<UserResponse> UpdateAsync(int id, CreateUserRequest request);
    Task DeleteAsync(int id);
} 