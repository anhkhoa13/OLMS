using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.ProgressAggregate;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations.UserModels;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Student");

        builder.Property(s => s.Major)
               .HasColumnName("Major")
               .HasMaxLength(100);

        builder.HasOne<UserBase>()
               .WithOne()
               .HasForeignKey<Student>(s => s.Id)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Progresses)
            .WithOne()
            .HasForeignKey(p => p.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Courses)
            .WithMany(c => c.Students);
    }
}
