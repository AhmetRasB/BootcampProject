using Data.Repositories.Abstracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concretes;

public class UserRepository : EfRepositoryBase<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> IsEmailExistAsync(string email, int? excludeId = null)
    {
        if (excludeId.HasValue)
            return await _dbSet.AnyAsync(u => u.Email == email && u.Id != excludeId.Value);
        
        return await _dbSet.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsNationalIdentityExistAsync(string nationalIdentity, int? excludeId = null)
    {
        if (excludeId.HasValue)
            return await _dbSet.AnyAsync(u => u.NationalityIdentity == nationalIdentity && u.Id != excludeId.Value);
        
        return await _dbSet.AnyAsync(u => u.NationalityIdentity == nationalIdentity);
    }

    public async Task<List<User>> GetActiveUsersAsync()
    {
        return await _dbSet.Where(u => u.DeletedDate == null).ToListAsync();
    }
}