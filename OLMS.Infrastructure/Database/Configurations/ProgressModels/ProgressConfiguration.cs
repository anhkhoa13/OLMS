using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.ProgressAggregate;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations.ProgressModels;

public sealed class ProgressConfiguration : IEntityTypeConfiguration<Progress>
{
    public void Configure(EntityTypeBuilder<Progress> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(p => p.StudentId);

        builder.HasOne<Course>()
            .WithMany()
            .HasForeignKey(p => p.CourseId);
    }
}
