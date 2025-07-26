using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Data.Configurations;

public class BootcampConfiguration : IEntityTypeConfiguration<Bootcamp>
{
    public void Configure(EntityTypeBuilder<Bootcamp> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired().HasMaxLength(200);
        builder.Property(b => b.InstructorId).IsRequired();
        builder.Property(b => b.StartDate).IsRequired();
        builder.Property(b => b.EndDate).IsRequired();
        builder.Property(b => b.BootcampState).IsRequired();
        
        // Relationships
        builder.HasOne(b => b.Instructor)
            .WithMany()
            .HasForeignKey(b => b.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Unique constraint for name
        builder.HasIndex(b => b.Name).IsUnique();
        
        // Check constraint for dates
        builder.ToTable(t => t.HasCheckConstraint("CK_Bootcamp_Dates", "StartDate < EndDate"));
    }
} 