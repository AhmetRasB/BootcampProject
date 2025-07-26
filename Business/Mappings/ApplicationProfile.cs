using AutoMapper;
using DTOs.Requests;
using DTOs.Responses;
using Entities;

namespace Business.Mappings;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<CreateApplicationRequest, Application>();
        CreateMap<Application, ApplicationResponse>()
            .ForMember(dest => dest.ApplicantName, 
                opt => opt.MapFrom(src => $"{src.Applicant.FirstName} {src.Applicant.LastName}"))
            .ForMember(dest => dest.BootcampName, 
                opt => opt.MapFrom(src => src.Bootcamp.Name));
    }
} 