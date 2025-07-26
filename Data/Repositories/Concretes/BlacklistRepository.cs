using Data.Repositories.Abstracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concretes;

public class BlacklistRepository : EfRepositoryBase<Blacklist>, IBlacklistRepository
{
    public BlacklistRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsApplicantActivelyBlacklistedAsync(int applicantId)
    {
        return await _dbSet.AnyAsync(b => b.ApplicantId == applicantId);
    }

    public async Task<Blacklist?> GetActiveBlacklistByApplicantAsync(int applicantId)
    {
        return await _dbSet
            .Include(b => b.Applicant)
            .FirstOrDefaultAsync(b => b.ApplicantId == applicantId);
    }
}