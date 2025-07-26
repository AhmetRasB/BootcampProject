using DTOs.Requests;
using DTOs.Responses;
using Entities.Enums;

namespace Business.Services.Abstracts;

public interface IApplicationService
{
    Task<ApplicationResponse> CreateAsync(CreateApplicationRequest request);
    Task<ApplicationResponse?> GetByIdAsync(int id);
    Task<List<ApplicationResponse>> GetAllAsync();
    Task<ApplicationResponse> UpdateApplicationStateAsync(int id, ApplicationState newState);
    Task DeleteAsync(int id);
} 