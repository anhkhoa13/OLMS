using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder) {
        builder.ToTable("Lessons");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Title)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(l => l.Content)
            .IsRequired();

        builder.Property(l => l.VideoUrl)
            .IsRequired()
            .HasMaxLength(2048);

        builder.Property(l => l.SectionId)
            .IsRequired();

        // Configure relationship with Attachments
        builder.HasMany(l => l.Attachments)
            .WithOne()
            .HasForeignKey("LessonId")  // The foreign key in Attachment table
            .OnDelete(DeleteBehavior.Cascade);

        // Configure privately owned collection
        builder.Metadata.FindNavigation(nameof(Lesson.Attachments))
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}


