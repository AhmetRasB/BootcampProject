using AutoMapper;
using DTOs.Requests;
using DTOs.Responses;
using Entities;

namespace Business.Mappings;

public class BootcampProfile : Profile
{
    public BootcampProfile()
    {
        CreateMap<CreateBootcampRequest, Bootcamp>();
        CreateMap<Bootcamp, BootcampResponse>()
            .ForMember(dest => dest.InstructorName, 
                opt => opt.MapFrom(src => $"{src.Instructor.FirstName} {src.Instructor.LastName}"));
    }
} 