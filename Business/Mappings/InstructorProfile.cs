using AutoMapper;
using DTOs.Requests;
using DTOs.Responses;
using Entities;

namespace Business.Mappings;

public class InstructorProfile : Profile
{
    public InstructorProfile()
    {
        CreateMap<CreateInstructorRequest, Instructor>();
        CreateMap<Instructor, InstructorResponse>();
    }
} 