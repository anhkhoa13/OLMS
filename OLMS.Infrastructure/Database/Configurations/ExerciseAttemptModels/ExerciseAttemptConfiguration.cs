
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.AssignmentAttempt;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations.ExerciseAttemptModels;

public class ExerciseAttemptConfiguration : IEntityTypeConfiguration<ExerciseAttempt>
{
    public void Configure(EntityTypeBuilder<ExerciseAttempt> builder)
    {
        builder.HasKey(ea => ea.Id);
        builder.Property(ea => ea.Score)
            .IsRequired()
            .HasColumnType("float");
        builder.Property(ea => ea.SubmitAt)
            .IsRequired()
            .HasColumnType("datetime");
        builder.Property(ea => ea.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnType("nvarchar(50)");
        builder.HasMany(ea => ea.SubmitAttachtment)
            .WithOne()
            .HasForeignKey(ea => ea.ExerciseAttemptId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(ea => ea.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Exercise>()
            .WithMany()
            .HasForeignKey(ea => ea.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
