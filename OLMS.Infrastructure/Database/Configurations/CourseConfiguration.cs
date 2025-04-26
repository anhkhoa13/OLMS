using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");

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
            .HasConversion<string>();

        builder.HasOne(i => i.Instructor)
            .WithMany()
            .HasForeignKey(c => c.InstructorId);

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
                       .OnDelete(DeleteBehavior.Restrict));


        //builder.HasOne(c => c.Instructor)  // Một Course có một Instructor
        //       .WithMany(i => i.Courses)
        //       .HasForeignKey(c => c.InstructorId)
        //       .IsRequired()
        //       .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(c => c.Enrollments)
        //      .WithOne()
        //      .HasForeignKey(e => e.CourseId)
        //      .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(c => c.QuizCourses)
        //       .WithOne(qc => qc.Course)
        //       .HasForeignKey(qc => qc.CourseId)
        //       .OnDelete(DeleteBehavior.Cascade);
    }
}

