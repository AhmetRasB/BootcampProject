using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Data.Configurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.ApplicantId).IsRequired();
        builder.Property(a => a.BootcampId).IsRequired();
        builder.Property(a => a.ApplicationState).IsRequired();
        
        // Relationships
        builder.HasOne(a => a.Applicant)
            .WithMany()
            .HasForeignKey(a => a.ApplicantId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(a => a.Bootcamp)
            .WithMany()
            .HasForeignKey(a => a.BootcampId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Unique constraint - one application per applicant per bootcamp
        builder.HasIndex(a => new { a.ApplicantId, a.BootcampId }).IsUnique();
    }
} 