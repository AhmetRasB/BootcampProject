using Entities;

namespace Data.Repositories.Abstracts;

public interface IBlacklistRepository : IRepository<Blacklist>
{
    Task<bool> IsApplicantActivelyBlacklistedAsync(int applicantId);
    Task<Blacklist?> GetActiveBlacklistByApplicantAsync(int applicantId);
}