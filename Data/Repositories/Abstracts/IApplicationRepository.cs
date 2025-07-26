using Entities;
using Entities.Enums;

namespace Data.Repositories.Abstracts;

public interface IApplicationRepository : IRepository<Application>
{
    Task<bool> HasApplicantAppliedToBootcampAsync(int applicantId, int bootcampId);
    Task<List<Application>> GetApplicationsByApplicantAsync(int applicantId);
    Task<List<Application>> GetApplicationsByBootcampAsync(int bootcampId);
    Task<List<Application>> GetApplicationsByStateAsync(ApplicationState state);
}