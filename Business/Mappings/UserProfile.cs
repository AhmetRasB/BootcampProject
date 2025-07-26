using AutoMapper;
using DTOs.Requests;
using DTOs.Responses;
using Entities;

namespace Business.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<User, UserResponse>();
    }
} 