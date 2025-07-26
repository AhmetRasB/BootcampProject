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

public class InstructorManager : IInstructorService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ApplicantBusinessRules _businessRules;

    public InstructorManager(IUserRepository userRepository, IMapper mapper, ApplicantBusinessRules businessRules)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<InstructorResponse> CreateAsync(CreateInstructorRequest request)
    {
        await _businessRules.CheckIfEmailExists(request.Email);
        await _businessRules.CheckIfNationalityIdentityExists(request.NationalityIdentity);

        var instructor = _mapper.Map<Instructor>(request);
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        instructor.PasswordHash = passwordHash;
        instructor.PasswordSalt = passwordSalt;

        var createdInstructor = await _userRepository.AddAsync(instructor);
        return _mapper.Map<InstructorResponse>(createdInstructor);
    }

    public async Task<InstructorResponse?> GetByIdAsync(int id)
    {
        var instructor = await _userRepository.GetByIdAsync(id) as Instructor;
        return instructor != null ? _mapper.Map<InstructorResponse>(instructor) : null;
    }

    public async Task<List<InstructorResponse>> GetAllAsync()
    {
        var instructors = await _userRepository.GetAllAsync();
        var instructorEntities = instructors.OfType<Instructor>().ToList();
        return _mapper.Map<List<InstructorResponse>>(instructorEntities);
    }

    public async Task<InstructorResponse> UpdateAsync(int id, CreateInstructorRequest request)
    {
        var instructor = await _userRepository.GetByIdAsync(id) as Instructor;
        if (instructor == null)
            throw new InvalidOperationException($"Instructor with ID {id} not found.");

        await _businessRules.CheckIfEmailExists(request.Email, id);
        await _businessRules.CheckIfNationalityIdentityExists(request.NationalityIdentity, id);

        instructor.FirstName = request.FirstName;
        instructor.LastName = request.LastName;
        instructor.DateOfBirth = request.DateOfBirth;
        instructor.NationalityIdentity = request.NationalityIdentity;
        instructor.Email = request.Email;
        instructor.CompanyName = request.CompanyName;

        if (!string.IsNullOrEmpty(request.Password))
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            instructor.PasswordHash = passwordHash;
            instructor.PasswordSalt = passwordSalt;
        }

        await _userRepository.UpdateAsync(instructor);
        return _mapper.Map<InstructorResponse>(instructor);
    }

    public async Task DeleteAsync(int id)
    {
        var instructor = await _userRepository.GetByIdAsync(id) as Instructor;
        if (instructor == null)
            throw new InvalidOperationException($"Instructor with ID {id} not found.");

        await _userRepository.DeleteAsync(instructor);
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
} 