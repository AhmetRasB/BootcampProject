using AutoMapper;
using Business.BusinessRules;
using Business.Services.Abstracts;
using Data.Repositories.Abstracts;
using DTOs.Requests;
using DTOs.Responses;
using Entities;

namespace Business.Services.Concretes;

public class BootcampManager : IBootcampService
{
    private readonly IBootcampRepository _bootcampRepository;
    private readonly IMapper _mapper;
    private readonly BootcampBusinessRules _businessRules;

    public BootcampManager(IBootcampRepository bootcampRepository, IMapper mapper, BootcampBusinessRules businessRules)
    {
        _bootcampRepository = bootcampRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<BootcampResponse> CreateAsync(CreateBootcampRequest request)
    {
        _businessRules.CheckIfStartDateIsBeforeEndDate(request.StartDate, request.EndDate);
        await _businessRules.CheckIfBootcampNameExists(request.Name);
        await _businessRules.CheckIfInstructorExists(request.InstructorId);

        var bootcamp = _mapper.Map<Bootcamp>(request);
        var createdBootcamp = await _bootcampRepository.AddAsync(bootcamp);
        return _mapper.Map<BootcampResponse>(createdBootcamp);
    }

    public async Task<BootcampResponse?> GetByIdAsync(int id)
    {
        var bootcamp = await _bootcampRepository.GetByIdAsync(id);
        return bootcamp != null ? _mapper.Map<BootcampResponse>(bootcamp) : null;
    }

    public async Task<List<BootcampResponse>> GetAllAsync()
    {
        var bootcamps = await _bootcampRepository.GetActiveBootcampAsync();
        return _mapper.Map<List<BootcampResponse>>(bootcamps);
    }

    public async Task<BootcampResponse> UpdateAsync(int id, CreateBootcampRequest request)
    {
        var bootcamp = await _bootcampRepository.GetByIdAsync(id);
        if (bootcamp == null)
            throw new InvalidOperationException($"Bootcamp with ID {id} not found.");

        _businessRules.CheckIfStartDateIsBeforeEndDate(request.StartDate, request.EndDate);
        await _businessRules.CheckIfBootcampNameExists(request.Name, id);
        await _businessRules.CheckIfInstructorExists(request.InstructorId);

        bootcamp.Name = request.Name;
        bootcamp.InstructorId = request.InstructorId;
        bootcamp.StartDate = request.StartDate;
        bootcamp.EndDate = request.EndDate;

        await _bootcampRepository.UpdateAsync(bootcamp);
        return _mapper.Map<BootcampResponse>(bootcamp);
    }

    public async Task DeleteAsync(int id)
    {
        var bootcamp = await _bootcampRepository.GetByIdAsync(id);
        if (bootcamp == null)
            throw new InvalidOperationException($"Bootcamp with ID {id} not found.");

        await _bootcampRepository.DeleteAsync(bootcamp);
    }
} 