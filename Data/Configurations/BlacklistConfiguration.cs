using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Data.Configurations;

public class BlacklistConfiguration : IEntityTypeConfiguration<Blacklist>
{
    public void Configure(EntityTypeBuilder<Blacklist> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Reason).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Date).IsRequired();
        builder.Property(b => b.ApplicantId).IsRequired();
        
        // Relationships
        builder.HasOne(b => b.Applicant)
            .WithMany()
            .HasForeignKey(b => b.ApplicantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
} 