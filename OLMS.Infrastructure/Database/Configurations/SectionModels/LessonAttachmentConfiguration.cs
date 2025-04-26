using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OLMS.Infrastructure.Database.Configurations.SectionModels;

public class LessonAttachmentConfiguration : IEntityTypeConfiguration<LessonAttachment>
{
    public void Configure(EntityTypeBuilder<LessonAttachment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Data)
            .IsRequired();

        builder.HasOne<Lesson>()
            .WithMany(l => l.LessonAttachments)
            .HasForeignKey(x => x.LessonId);
    }
}

