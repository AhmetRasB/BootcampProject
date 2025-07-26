using Data.Repositories.Abstracts;
using Entities;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concretes;

public class ApplicationRepository : EfRepositoryBase<Application>, IApplicationRepository
{
    public ApplicationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> HasApplicantAppliedToBootcampAsync(int applicantId, int bootcampId)
    {
        return await _dbSet.AnyAsync(a => a.ApplicantId == applicantId && a.BootcampId == bootcampId);
    }

    public async Task<List<Application>> GetApplicationsByApplicantAsync(int applicantId)
    {
        return await _dbSet
            .Include(a => a.Bootcamp)
            .Include(a => a.Applicant)
            .Where(a => a.ApplicantId == applicantId)
            .ToListAsync();
    }

    public async Task<List<Application>> GetApplicationsByBootcampAsync(int bootcampId)
    {
        return await _dbSet
            .Include(a => a.Applicant)
            .Include(a => a.Bootcamp)
            .Where(a => a.BootcampId == bootcampId)
            .ToListAsync();
    }

    public async Task<List<Application>> GetApplicationsByStateAsync(ApplicationState state)
    {
        return await _dbSet
            .Include(a => a.Applicant)
            .Include(a => a.Bootcamp)
            .Where(a => a.ApplicationState == state)
            .ToListAsync();
    }
}