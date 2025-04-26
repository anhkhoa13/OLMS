using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.QuestionEntity;

namespace OLMS.Infrastructure.Database.Configurations.AssignmentModels.QuizModels;

public sealed class ShortAnswerQuestionConfiguration : IEntityTypeConfiguration<ShortAnswerQuestion>
{
    public void Configure(EntityTypeBuilder<ShortAnswerQuestion> builder)
    {

        builder.Property(q => q.CorrectAnswer)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(q => q.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne<Question>()
            .WithOne()
            .HasForeignKey<ShortAnswerQuestion>(saq => saq.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

