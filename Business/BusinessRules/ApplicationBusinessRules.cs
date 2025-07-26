using Data.Repositories.Abstracts;
using Entities;
using Entities.Enums;

namespace Business.BusinessRules;

public class ApplicationBusinessRules
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBootcampRepository _bootcampRepository;
    private readonly IBlacklistRepository _blacklistRepository;

    public ApplicationBusinessRules(
        IApplicationRepository applicationRepository,
        IBootcampRepository bootcampRepository,
        IBlacklistRepository blacklistRepository)
    {
        _applicationRepository = applicationRepository;
        _bootcampRepository = bootcampRepository;
        _blacklistRepository = blacklistRepository;
    }

    public async Task CheckIfApplicantAlreadyApplied(int applicantId, int bootcampId)
    {
        var hasApplied = await _applicationRepository.HasApplicantAppliedToBootcampAsync(applicantId, bootcampId);
        if (hasApplied)
            throw new InvalidOperationException($"Applicant has already applied to this bootcamp.");
    }

    public async Task CheckIfBootcampIsActive(int bootcampId)
    {
        var bootcamp = await _bootcampRepository.GetByIdAsync(bootcampId);
        if (bootcamp == null)
            throw new InvalidOperationException($"Bootcamp with ID {bootcampId} not found.");
        
        if (bootcamp.BootcampState != BootcampState.OPEN_FOR_APPLICATION)
            throw new InvalidOperationException("Bootcamp is not open for applications.");
    }

    public async Task CheckIfApplicantIsBlacklisted(int applicantId)
    {
        var isBlacklisted = await _blacklistRepository.IsApplicantActivelyBlacklistedAsync(applicantId);
        if (isBlacklisted)
            throw new InvalidOperationException($"Applicant is blacklisted and cannot apply.");
    }

    public void CheckIfStateTransitionIsValid(ApplicationState currentState, ApplicationState newState)
    {
        var validTransitions = new Dictionary<ApplicationState, ApplicationState[]>
        {
            { ApplicationState.PENDING, new[] { ApplicationState.APPROVED, ApplicationState.REJECTED, ApplicationState.IN_REVIEW, ApplicationState.CANCELLED } },
            { ApplicationState.IN_REVIEW, new[] { ApplicationState.APPROVED, ApplicationState.REJECTED, ApplicationState.CANCELLED } },
            { ApplicationState.APPROVED, new[] { ApplicationState.CANCELLED } },
            { ApplicationState.REJECTED, new[] { ApplicationState.CANCELLED } },
            { ApplicationState.CANCELLED, new ApplicationState[] { } }
        };

        if (!validTransitions.ContainsKey(currentState) || 
            !validTransitions[currentState].Contains(newState))
        {
            throw new InvalidOperationException($"Invalid state transition from {currentState} to {newState}.");
        }
    }
} 