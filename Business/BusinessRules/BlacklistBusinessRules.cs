using Data.Repositories.Abstracts;
using Entities;

namespace Business.BusinessRules;

public class BlacklistBusinessRules
{
    private readonly IBlacklistRepository _blacklistRepository;
    private readonly IUserRepository _userRepository;

    public BlacklistBusinessRules(IBlacklistRepository blacklistRepository, IUserRepository userRepository)
    {
        _blacklistRepository = blacklistRepository;
        _userRepository = userRepository;
    }

    public async Task CheckIfApplicantAlreadyBlacklisted(int applicantId)
    {
        var isBlacklisted = await _blacklistRepository.IsApplicantActivelyBlacklistedAsync(applicantId);
        if (isBlacklisted)
            throw new InvalidOperationException($"Applicant is already blacklisted.");
    }

    public void CheckIfReasonIsNotEmpty(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new InvalidOperationException("Blacklist reason cannot be empty.");
    }

    public async Task CheckIfApplicantExists(int applicantId)
    {
        var applicant = await _userRepository.GetByIdAsync(applicantId);
        if (applicant == null)
            throw new InvalidOperationException($"Applicant with ID {applicantId} not found.");
    }
} 