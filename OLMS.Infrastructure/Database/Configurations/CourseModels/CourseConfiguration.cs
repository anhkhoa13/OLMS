using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.ForumAggregate;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations.CourseModels;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id); // Khóa chính

        builder.OwnsOne(c => c.Code, c =>
        {
            c.Property(c => c.Value)
             .HasColumnName("Code")
             .IsRequired()
             .HasMaxLength(6);

            c.HasIndex(c => c.Value).IsUnique();
        });

        builder.Property(c => c.Title)
               .IsRequired()
               .HasMaxLength(255);
        builder.Property(c => c.Description)
               .HasMaxLength(1000);

        builder.Property(c => c.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);

        builder.HasMany(c => c.Students)
               .WithMany(s => s.Courses)
               .UsingEntity<Dictionary<string, object>>(
                   "CourseStudent",
                   j => j
                       .HasOne<Student>()
                       .WithMany()
                       .HasForeignKey("StudentsId")
                       .OnDelete(DeleteBehavior.Restrict),
                   j => j
                       .HasOne<Course>()
                       .WithMany()
                       .HasForeignKey("CoursesId")
                       .OnDelete(DeleteBehavior.Restrict)
                );

        
        builder.HasOne<Forum>()
               .WithOne()
               .HasForeignKey<Course>(c => c.ForumId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Instructor)
               .WithMany(i => i.Courses)
               .HasForeignKey(c => c.InstructorId)
               .IsRequired();

        builder.HasMany(c => c.Sections)
                .WithOne()
                .HasForeignKey(s => s.CourseId)
                .IsRequired();
    }
}

