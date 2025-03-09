using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Student");

        //builder.Property(s => s.Id).HasColumnName("Id_student");
        builder.Property(s => s.Major)
               .HasColumnName("Major")
               .HasMaxLength(100);
        builder.Property(s => s.EnrollmentDate)
               .HasColumnName("EnrollmentDate");
        
        builder.HasOne<UserBase>()
               .WithOne()
               .HasForeignKey<Student>(s => s.Id)
               .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(s => s.Enrollments)
        //       .WithOne()
        //       .HasForeignKey(e => e.StudentId)
        //       .OnDelete(DeleteBehavior.Cascade);
    }
}
