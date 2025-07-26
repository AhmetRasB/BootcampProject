using AutoMapper;
using Business.BusinessRules;
using Business.Services.Abstracts;
using Data.Repositories.Abstracts;
using DTOs.Requests;
using DTOs.Responses;
using Entities;
using Entities.Enums;

namespace Business.Services.Concretes;

public class ApplicationManager : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationBusinessRules _businessRules;

    public ApplicationManager(IApplicationRepository applicationRepository, IMapper mapper, ApplicationBusinessRules businessRules)
    {
        _applicationRepository = applicationRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<ApplicationResponse> CreateAsync(CreateApplicationRequest request)
    {
        await _businessRules.CheckIfApplicantAlreadyApplied(request.ApplicantId, request.BootcampId);
        await _businessRules.CheckIfBootcampIsActive(request.BootcampId);
        await _businessRules.CheckIfApplicantIsBlacklisted(request.ApplicantId);

        var application = _mapper.Map<Application>(request);
        var createdApplication = await _applicationRepository.AddAsync(application);
        return _mapper.Map<ApplicationResponse>(createdApplication);
    }

    public async Task<ApplicationResponse?> GetByIdAsync(int id)
    {
        var application = await _applicationRepository.GetByIdAsync(id);
        return application != null ? _mapper.Map<ApplicationResponse>(application) : null;
    }

    public async Task<List<ApplicationResponse>> GetAllAsync()
    {
        var applications = await _applicationRepository.GetAllAsync();
        return _mapper.Map<List<ApplicationResponse>>(applications);
    }

    public async Task<ApplicationResponse> UpdateApplicationStateAsync(int id, ApplicationState newState)
    {
        var application = await _applicationRepository.GetByIdAsync(id);
        if (application == null)
            throw new InvalidOperationException($"Application with ID {id} not found.");

        _businessRules.CheckIfStateTransitionIsValid(application.ApplicationState, newState);
        application.ApplicationState = newState;

        await _applicationRepository.UpdateAsync(application);
        return _mapper.Map<ApplicationResponse>(application);
    }

    public async Task DeleteAsync(int id)
    {
        var application = await _applicationRepository.GetByIdAsync(id);
        if (application == null)
            throw new InvalidOperationException($"Application with ID {id} not found.");

        await _applicationRepository.DeleteAsync(application);
    }
} 