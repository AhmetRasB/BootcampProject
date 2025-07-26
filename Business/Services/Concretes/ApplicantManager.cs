using AutoMapper;
using Business.BusinessRules;
using Business.Services.Abstracts;
using Data.Repositories.Abstracts;
using DTOs.Requests;
using DTOs.Responses;
using Entities;
using System.Security.Cryptography;
using System.Text;

namespace Business.Services.Concretes;

public class ApplicantManager : IApplicantService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ApplicantBusinessRules _businessRules;

    public ApplicantManager(IUserRepository userRepository, IMapper mapper, ApplicantBusinessRules businessRules)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<ApplicantResponse> CreateAsync(CreateApplicantRequest request)
    {
        await _businessRules.CheckIfEmailExists(request.Email);
        await _businessRules.CheckIfNationalityIdentityExists(request.NationalityIdentity);

        var applicant = _mapper.Map<Applicant>(request);
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        applicant.PasswordHash = passwordHash;
        applicant.PasswordSalt = passwordSalt;

        var createdApplicant = await _userRepository.AddAsync(applicant);
        return _mapper.Map<ApplicantResponse>(createdApplicant);
    }

    public async Task<ApplicantResponse?> GetByIdAsync(int id)
    {
        var applicant = await _userRepository.GetByIdAsync(id) as Applicant;
        return applicant != null ? _mapper.Map<ApplicantResponse>(applicant) : null;
    }

    public async Task<List<ApplicantResponse>> GetAllAsync()
    {
        var applicants = await _userRepository.GetAllAsync();
        var applicantEntities = applicants.OfType<Applicant>().ToList();
        return _mapper.Map<List<ApplicantResponse>>(applicantEntities);
    }

    public async Task<ApplicantResponse> UpdateAsync(int id, CreateApplicantRequest request)
    {
        var applicant = await _userRepository.GetByIdAsync(id) as Applicant;
        if (applicant == null)
            throw new InvalidOperationException($"Applicant with ID {id} not found.");

        await _businessRules.CheckIfEmailExists(request.Email, id);
        await _businessRules.CheckIfNationalityIdentityExists(request.NationalityIdentity, id);

        applicant.FirstName = request.FirstName;
        applicant.LastName = request.LastName;
        applicant.DateOfBirth = request.DateOfBirth;
        applicant.NationalityIdentity = request.NationalityIdentity;
        applicant.Email = request.Email;
        applicant.About = request.About;

        if (!string.IsNullOrEmpty(request.Password))
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            applicant.PasswordHash = passwordHash;
            applicant.PasswordSalt = passwordSalt;
        }

        await _userRepository.UpdateAsync(applicant);
        return _mapper.Map<ApplicantResponse>(applicant);
    }

    public async Task DeleteAsync(int id)
    {
        var applicant = await _userRepository.GetByIdAsync(id) as Applicant;
        if (applicant == null)
            throw new InvalidOperationException($"Applicant with ID {id} not found.");

        await _userRepository.DeleteAsync(applicant);
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
} 