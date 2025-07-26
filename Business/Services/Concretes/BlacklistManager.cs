using AutoMapper;
using Business.BusinessRules;
using Business.Services.Abstracts;
using Data.Repositories.Abstracts;
using DTOs.Requests;
using DTOs.Responses;
using Entities;

namespace Business.Services.Concretes;

public class BlacklistManager : IBlacklistService
{
    private readonly IBlacklistRepository _blacklistRepository;
    private readonly IMapper _mapper;
    private readonly BlacklistBusinessRules _businessRules;

    public BlacklistManager(IBlacklistRepository blacklistRepository, IMapper mapper, BlacklistBusinessRules businessRules)
    {
        _blacklistRepository = blacklistRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<BlacklistResponse> CreateAsync(CreateBlacklistRequest request)
    {
        _businessRules.CheckIfReasonIsNotEmpty(request.Reason);
        await _businessRules.CheckIfApplicantExists(request.ApplicantId);
        await _businessRules.CheckIfApplicantAlreadyBlacklisted(request.ApplicantId);

        var blacklist = new Blacklist
        {
            Reason = request.Reason,
            Date = DateTime.Now,
            ApplicantId = request.ApplicantId
        };

        var createdBlacklist = await _blacklistRepository.AddAsync(blacklist);
        return _mapper.Map<BlacklistResponse>(createdBlacklist);
    }

    public async Task<BlacklistResponse?> GetByIdAsync(int id)
    {
        var blacklist = await _blacklistRepository.GetByIdAsync(id);
        return blacklist != null ? _mapper.Map<BlacklistResponse>(blacklist) : null;
    }

    public async Task<List<BlacklistResponse>> GetAllAsync()
    {
        var blacklists = await _blacklistRepository.GetAllAsync();
        return _mapper.Map<List<BlacklistResponse>>(blacklists);
    }

    public async Task DeleteAsync(int id)
    {
        var blacklist = await _blacklistRepository.GetByIdAsync(id);
        if (blacklist == null)
            throw new InvalidOperationException($"Blacklist with ID {id} not found.");

        await _blacklistRepository.DeleteAsync(blacklist);
    }
} 