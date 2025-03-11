using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Primitives;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class MultipleChoiceQuizConfiguration : IEntityTypeConfiguration<MultipleChoiceQuiz>
{
    public void Configure(EntityTypeBuilder<MultipleChoiceQuiz> builder)
    {
        builder.ToTable("MultipleChoiceQuiz");

        builder.Property(mcq => mcq.TimeLimit)
            .HasConversion(v => v.ToTimeSpan(), v => TimeOnly.FromTimeSpan(v));

        builder.HasOne<Quiz>()
            .WithOne()
            .HasForeignKey<MultipleChoiceQuiz>(mcq => mcq.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

