using Data.Repositories.Abstracts;
using Entities;

namespace Business.BusinessRules;

public class BootcampBusinessRules
{
    private readonly IBootcampRepository _bootcampRepository;
    private readonly IUserRepository _userRepository;

    public BootcampBusinessRules(IBootcampRepository bootcampRepository, IUserRepository userRepository)
    {
        _bootcampRepository = bootcampRepository;
        _userRepository = userRepository;
    }

    public void CheckIfStartDateIsBeforeEndDate(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
            throw new InvalidOperationException("Start date must be before end date.");
    }

    public async Task CheckIfBootcampNameExists(string name, int? excludeId = null)
    {
        var exists = await _bootcampRepository.IsBootcampNameExistAsync(name, excludeId);
        if (exists)
            throw new InvalidOperationException($"A bootcamp with name '{name}' already exists.");
    }

    public async Task CheckIfInstructorExists(int instructorId)
    {
        var instructor = await _userRepository.GetByIdAsync(instructorId);
        if (instructor == null)
            throw new InvalidOperationException($"Instructor with ID {instructorId} not found.");
    }

    public void CheckIfBootcampIsOpenForApplication(Entities.Enums.BootcampState state)
    {
        if (state != Entities.Enums.BootcampState.OPEN_FOR_APPLICATION)
            throw new InvalidOperationException("Bootcamp is not open for applications.");
    }
} 