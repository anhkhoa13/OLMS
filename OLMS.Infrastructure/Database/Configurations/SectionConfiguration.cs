using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Section");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Title).IsRequired();
        builder.Property(s => s.CourseId).IsRequired();

        builder.HasMany(s => s.Lessons)
               .WithOne()
               .HasForeignKey(l => l.SectionId);

        builder.HasMany(s => s.SectionItems)
               .WithOne()
               .HasForeignKey(si => si.SectionId);

        builder.HasMany(s => s.Assignments)
               .WithOne()
               .HasForeignKey(si => si.SectionId);
    }
}

