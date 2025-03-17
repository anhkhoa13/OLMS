using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Primitives;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class QuizAttemptConfiguration : IEntityTypeConfiguration<QuizAttempt>
{
    public void Configure(EntityTypeBuilder<QuizAttempt> builder)
    {
        builder.ToTable("QuizAttempt");

        builder.Property(q => q.StudentId)
               .IsRequired();

        builder.Property(q => q.QuizId)
               .IsRequired();

        builder.Property(q => q.SubmittedAt)
               .IsRequired(false); // Nullable in case quiz is not submitted yet

        builder.Property(q => q.StartTime)
               .IsRequired();

        builder.Property(q => q.Status)
               .HasConversion<int>() // Store Enum as int
               .IsRequired();

        builder.Property(q => q.Score)
               .HasDefaultValue(0.0);

        // One-to-Many: A QuizAttempt can have multiple StudentAnswers
        builder.HasMany(q => q.Answers)
               .WithOne(a => a.QuizAttempt)
               .HasForeignKey(a => a.QuizAttemptId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

