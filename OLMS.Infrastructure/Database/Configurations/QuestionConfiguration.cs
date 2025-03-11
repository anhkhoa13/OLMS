using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Primitives;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Question");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Content)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(q => q.Type)
            .HasColumnName("QuestionType")
            .HasConversion<string>() // Store Enum as string
            .IsRequired();

        builder.HasOne<Quiz>()
            .WithMany()
            .HasForeignKey(q => q.QuizId);
    }
}

