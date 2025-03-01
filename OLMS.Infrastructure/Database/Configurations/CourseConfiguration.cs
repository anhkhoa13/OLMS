using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");

        builder.HasKey(c => c.Id); // Khóa chính

        builder.Property(c => c.Title)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(c => c.Description)
               .HasMaxLength(1000);

        builder.HasOne(c => c.Instructor)  // Một Course có một Instructor
               .WithMany(i => i.Courses)
               .HasForeignKey(c => c.InstructorId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

    }
}

