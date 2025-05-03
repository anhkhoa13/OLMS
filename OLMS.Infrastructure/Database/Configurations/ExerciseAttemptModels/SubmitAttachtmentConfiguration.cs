

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.AssignmentAttempt;

namespace OLMS.Infrastructure.Database.Configurations.ExerciseAttemptModels;

public class SubmitAttachtmentConfiguration : IEntityTypeConfiguration<SubmitAttachment>
{
    public void Configure(EntityTypeBuilder<SubmitAttachment> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(a => a.Type)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(a => a.Data)
            .IsRequired();

        builder.HasOne<ExerciseAttempt>()
            .WithMany(e => e.SubmitAttachtment)
            .HasForeignKey(a => a.ExerciseAttemptId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
