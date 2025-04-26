using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OLMS.Infrastructure.Database.Configurations.AssignmentModels.ExerciseModels;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasMany(e => e.ExerciseAttachments)
            .WithOne()
            .HasForeignKey(a => a.ExerciseId);

    }
}

