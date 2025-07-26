using AutoMapper;
using DTOs.Requests;
using DTOs.Responses;
using Entities;

namespace Business.Mappings;

public class BlacklistProfile : Profile
{
    public BlacklistProfile()
    {
        CreateMap<CreateBlacklistRequest, Blacklist>();
        CreateMap<Blacklist, BlacklistResponse>()
            .ForMember(dest => dest.ApplicantName, 
                opt => opt.MapFrom(src => $"{src.Applicant.FirstName} {src.Applicant.LastName}"));
    }
} 