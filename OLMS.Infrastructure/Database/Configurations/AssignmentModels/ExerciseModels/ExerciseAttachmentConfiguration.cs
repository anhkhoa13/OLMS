using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Primitives;

namespace OLMS.Infrastructure.Database.Configurations.AssignmentModels.ExerciseModels;

public class ExerciseAttachmentConfiguration : IEntityTypeConfiguration<ExerciseAttachment>
{
    public void Configure(EntityTypeBuilder<ExerciseAttachment> builder)
    {

        builder.HasKey(a => a.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(a => a.Type)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Data)
            .IsRequired();

        builder.HasOne<Exercise>()
            .WithMany(e => e.ExerciseAttachments)
            .HasForeignKey(a => a.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
