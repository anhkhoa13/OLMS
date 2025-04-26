using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.QuestionEntity;

namespace OLMS.Infrastructure.Database.Configurations.AssignmentModels.QuizModels;

public sealed class MultipleChoiceQuestionConfiguration : IEntityTypeConfiguration<MultipleChoiceQuestion>
{
    public void Configure(EntityTypeBuilder<MultipleChoiceQuestion> builder)
    {

        builder.Property(mcq => mcq.CorrectOptionIndex)
            .IsRequired();
        
        builder.Property(mcq => mcq.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(mcq => mcq.Options)
            .HasConversion(
                v => string.Join(";", v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
            )
            .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
                c => c == null ? 0 : c.Aggregate(0, (a, v) => HashCode.Combine(a, v != null ? v.GetHashCode() : 0)),
                c => c == null ? new List<string>() : c.ToList()
            ));


        builder.HasOne<Question>()
            .WithOne()
            .HasForeignKey<MultipleChoiceQuestion>(mcq => mcq.Id)
            .OnDelete(DeleteBehavior.Cascade);

    }
}

