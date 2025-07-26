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

public class UserManager : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ApplicantBusinessRules _businessRules;

    public UserManager(IUserRepository userRepository, IMapper mapper, ApplicantBusinessRules businessRules)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<UserResponse> CreateAsync(CreateUserRequest request)
    {
        await _businessRules.CheckIfEmailExists(request.Email);
        await _businessRules.CheckIfNationalityIdentityExists(request.NationalityIdentity);

        var user = _mapper.Map<User>(request);
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        var createdUser = await _userRepository.AddAsync(user);
        return _mapper.Map<UserResponse>(createdUser);
    }

    public async Task<UserResponse?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user != null ? _mapper.Map<UserResponse>(user) : null;
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<List<UserResponse>>(users);
    }

    public async Task<UserResponse> UpdateAsync(int id, CreateUserRequest request)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new InvalidOperationException($"User with ID {id} not found.");

        await _businessRules.CheckIfEmailExists(request.Email, id);
        await _businessRules.CheckIfNationalityIdentityExists(request.NationalityIdentity, id);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.DateOfBirth = request.DateOfBirth;
        user.NationalityIdentity = request.NationalityIdentity;
        user.Email = request.Email;

        if (!string.IsNullOrEmpty(request.Password))
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }

        await _userRepository.UpdateAsync(user);
        return _mapper.Map<UserResponse>(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new InvalidOperationException($"User with ID {id} not found.");

        await _userRepository.DeleteAsync(user);
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
} 