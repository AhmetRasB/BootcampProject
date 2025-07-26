using AutoMapper;
using DTOs.Requests;
using DTOs.Responses;
using Entities;

namespace Business.Mappings;

public class ApplicantProfile : Profile
{
    public ApplicantProfile()
    {
        CreateMap<CreateApplicantRequest, Applicant>();
        CreateMap<Applicant, ApplicantResponse>();
    }
} 