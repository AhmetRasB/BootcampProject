using Data.Repositories.Abstracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concretes;

public class BootcampRepository : EfRepositoryBase<Bootcamp>, IBootcampRepository
{
    public BootcampRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsBootcampNameExistAsync(string name, int? excludeId = null)
    {
        if (excludeId.HasValue)
            return await _dbSet.AnyAsync(b => b.Name == name && b.Id != excludeId.Value);
        
        return await _dbSet.AnyAsync(b => b.Name == name);
    }

    public async Task<List<Bootcamp>> GetActiveBootcampAsync()
    {
        return await _dbSet
            .Include(b => b.Instructor)
            .Where(b => b.DeletedDate == null)
            .ToListAsync();
    }

    public async Task<List<Bootcamp>> GetBootcampByInstructorAsync(int instructorId)
    {
        return await _dbSet
            .Include(b => b.Instructor)
            .Where(b => b.InstructorId == instructorId && b.DeletedDate == null)
            .ToListAsync();
    }
}