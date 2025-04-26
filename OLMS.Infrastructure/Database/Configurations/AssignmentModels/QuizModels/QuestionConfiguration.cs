using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Entities.QuizEntity;

namespace OLMS.Infrastructure.Database.Configurations.AssignmentModels.QuizModels;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        //builder.ToTable("Questions");
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Content)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(q => q.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);


        builder.Property(q => q.Type)
            .HasConversion<string>() // Store Enum as string
            .IsRequired();

        builder.HasOne<Quiz>()
            .WithMany()
            .HasForeignKey(q => q.QuizId);


    }
}

