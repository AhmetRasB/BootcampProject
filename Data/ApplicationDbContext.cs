using Microsoft.EntityFrameworkCore;
using Entities;

namespace Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Bootcamp> Bootcamps { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<Blacklist> Blacklists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply all configurations from Configurations folder
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}