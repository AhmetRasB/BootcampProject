using Business.Services.Abstracts;
using DTOs.Requests;
using Entities.Enums;
using System.Security.Cryptography;
using System.Text;

namespace Business.Services.Concretes;

public class SeedDataService
{
    private readonly IInstructorService _instructorService;
    private readonly IApplicantService _applicantService;
    private readonly IBootcampService _bootcampService;
    private readonly IApplicationService _applicationService;

    public SeedDataService(
        IInstructorService instructorService,
        IApplicantService applicantService,
        IBootcampService bootcampService,
        IApplicationService applicationService)
    {
        _instructorService = instructorService;
        _applicantService = applicantService;
        _bootcampService = bootcampService;
        _applicationService = applicationService;
    }

    public async Task SeedDataAsync()
    {
        // Create Instructors
        var instructor1 = await _instructorService.CreateAsync(new CreateInstructorRequest
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1985, 5, 15),
            NationalityIdentity = "12345678901",
            Email = "john.doe@techacademy.com",
            Password = "password123",
            CompanyName = "Tech Academy"
        });

        var instructor2 = await _instructorService.CreateAsync(new CreateInstructorRequest
        {
            FirstName = "Jane",
            LastName = "Smith",
            DateOfBirth = new DateTime(1990, 8, 22),
            NationalityIdentity = "12345678902",
            Email = "jane.smith@codecamp.com",
            Password = "password123",
            CompanyName = "Code Camp Pro"
        });

        // Create Applicants
        var applicant1 = await _applicantService.CreateAsync(new CreateApplicantRequest
        {
            FirstName = "Alice",
            LastName = "Johnson",
            DateOfBirth = new DateTime(1995, 3, 10),
            NationalityIdentity = "12345678903",
            Email = "alice.johnson@email.com",
            Password = "password123",
            About = "Passionate about web development and eager to learn new technologies."
        });

        var applicant2 = await _applicantService.CreateAsync(new CreateApplicantRequest
        {
            FirstName = "Bob",
            LastName = "Wilson",
            DateOfBirth = new DateTime(1992, 11, 5),
            NationalityIdentity = "12345678904",
            Email = "bob.wilson@email.com",
            Password = "password123",
            About = "Interested in mobile app development and UI/UX design."
        });

        var applicant3 = await _applicantService.CreateAsync(new CreateApplicantRequest
        {
            FirstName = "Carol",
            LastName = "Brown",
            DateOfBirth = new DateTime(1998, 7, 18),
            NationalityIdentity = "12345678905",
            Email = "carol.brown@email.com",
            Password = "password123",
            About = "Looking to transition into a career in software development."
        });

        // Create Bootcamps
        var bootcamp1 = await _bootcampService.CreateAsync(new CreateBootcampRequest
        {
            Name = "Full Stack Web Development",
            InstructorId = instructor1.Id,
            StartDate = DateTime.Now.AddDays(30),
            EndDate = DateTime.Now.AddDays(90)
        });

        var bootcamp2 = await _bootcampService.CreateAsync(new CreateBootcampRequest
        {
            Name = "Mobile App Development with React Native",
            InstructorId = instructor2.Id,
            StartDate = DateTime.Now.AddDays(45),
            EndDate = DateTime.Now.AddDays(105)
        });

        var bootcamp3 = await _bootcampService.CreateAsync(new CreateBootcampRequest
        {
            Name = "Data Science Fundamentals",
            InstructorId = instructor1.Id,
            StartDate = DateTime.Now.AddDays(60),
            EndDate = DateTime.Now.AddDays(120)
        });

        // Create Applications
        await _applicationService.CreateAsync(new CreateApplicationRequest
        {
            ApplicantId = applicant1.Id,
            BootcampId = bootcamp1.Id
        });

        await _applicationService.CreateAsync(new CreateApplicationRequest
        {
            ApplicantId = applicant2.Id,
            BootcampId = bootcamp1.Id
        });

        await _applicationService.CreateAsync(new CreateApplicationRequest
        {
            ApplicantId = applicant1.Id,
            BootcampId = bootcamp2.Id
        });

        await _applicationService.CreateAsync(new CreateApplicationRequest
        {
            ApplicantId = applicant3.Id,
            BootcampId = bootcamp3.Id
        });
    }
} 