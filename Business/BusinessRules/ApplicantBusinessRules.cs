using Data.Repositories.Abstracts;
using Entities;

namespace Business.BusinessRules;

public class ApplicantBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IBlacklistRepository _blacklistRepository;

    public ApplicantBusinessRules(IUserRepository userRepository, IBlacklistRepository blacklistRepository)
    {
        _userRepository = userRepository;
        _blacklistRepository = blacklistRepository;
    }

    public async Task CheckIfNationalityIdentityExists(string nationalityIdentity, int? excludeId = null)
    {
        var exists = await _userRepository.IsNationalIdentityExistAsync(nationalityIdentity, excludeId);
        if (exists)
            throw new InvalidOperationException($"A user with nationality identity '{nationalityIdentity}' already exists.");
    }

    public async Task CheckIfEmailExists(string email, int? excludeId = null)
    {
        var exists = await _userRepository.IsEmailExistAsync(email, excludeId);
        if (exists)
            throw new InvalidOperationException($"A user with email '{email}' already exists.");
    }

    public async Task CheckIfApplicantExists(int applicantId)
    {
        var applicant = await _userRepository.GetByIdAsync(applicantId);
        if (applicant == null)
            throw new InvalidOperationException($"Applicant with ID {applicantId} not found.");
    }

    public async Task CheckIfApplicantIsBlacklisted(int applicantId)
    {
        var isBlacklisted = await _blacklistRepository.IsApplicantActivelyBlacklistedAsync(applicantId);
        if (isBlacklisted)
            throw new InvalidOperationException($"Applicant with ID {applicantId} is blacklisted and cannot perform this action.");
    }
} 