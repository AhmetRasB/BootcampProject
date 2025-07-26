using Business.BusinessRules;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Data;
using Data.Repositories.Abstracts;
using Data.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
        b => b.MigrationsAssembly("Data")));

// AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Repository Registrations
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBootcampRepository, BootcampRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IBlacklistRepository, BlacklistRepository>();

// Business Rules Registrations
builder.Services.AddScoped<ApplicantBusinessRules>();
builder.Services.AddScoped<BootcampBusinessRules>();
builder.Services.AddScoped<ApplicationBusinessRules>();
builder.Services.AddScoped<BlacklistBusinessRules>();

// Service Registrations
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IApplicantService, ApplicantManager>();
builder.Services.AddScoped<IInstructorService, InstructorManager>();
builder.Services.AddScoped<IBootcampService, BootcampManager>();
builder.Services.AddScoped<IApplicationService, ApplicationManager>();
builder.Services.AddScoped<IBlacklistService, BlacklistManager>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<SeedDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandler>();
app.UseAuthorization();
app.MapControllers();

app.Run();