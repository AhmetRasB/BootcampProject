using Entities;

namespace Data.Repositories.Abstracts;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> IsEmailExistAsync(string email, int? excludeId = null);
    Task<bool> IsNationalIdentityExistAsync(string nationalIdentity, int? excludeId = null);
    Task<List<User>> GetActiveUsersAsync();
}