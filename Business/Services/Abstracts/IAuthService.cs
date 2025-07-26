using DTOs.Requests;
using DTOs.Responses;

namespace Business.Services.Abstracts;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> RegisterAsync(CreateUserRequest request);
} 