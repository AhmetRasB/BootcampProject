using Entities;

namespace Data.Repositories.Abstracts;

public interface IBootcampRepository : IRepository<Bootcamp>
{
    Task<bool> IsBootcampNameExistAsync(string name, int? excludeId = null);
    Task<List<Bootcamp>> GetActiveBootcampAsync();
    Task<List<Bootcamp>> GetBootcampByInstructorAsync(int instructorId);
}