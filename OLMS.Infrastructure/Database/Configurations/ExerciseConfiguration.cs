using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OLMS.Infrastructure.Database.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise> {
    public void Configure(EntityTypeBuilder<Exercise> builder) {
        builder.ToTable("Exercises");
        // Configure the relationship with Attachments
        builder.HasMany(e => e.Attachments)
            .WithOne()
            .HasForeignKey(a => a.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure field-backed collection
        builder.Metadata.FindNavigation(nameof(Exercise.Attachments))
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

