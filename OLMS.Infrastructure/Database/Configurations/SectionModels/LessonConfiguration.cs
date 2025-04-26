using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations.SectionModels;

public sealed class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Title)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(l => l.Content)
            .IsRequired();

        builder.Property(l => l.VideoUrl)
            .IsRequired()
            .HasMaxLength(2048);

        builder.HasOne<Section>()
            .WithMany()
            .HasForeignKey(l => l.SectionId);

        builder.HasMany(l => l.LessonAttachments)
            .WithOne()
            .HasForeignKey(la => la.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


