using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Primitives;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.ToTable("Quiz");

        builder.HasKey(q => q.Id);
        builder.OwnsOne(c => c.Code, c =>
        {
            c.Property(c => c.Value)
             .HasColumnName("Code")
             .IsRequired()
             .HasMaxLength(6);

            c.HasIndex(c => c.Value).IsUnique();
        });
        builder.Property(q => q.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(q => q.Description)
            .HasMaxLength(2000);

        builder.Property(q => q.StartTime)
            .IsRequired();

        builder.Property(q => q.EndTime)
            .IsRequired();

        builder.Property(q => q.IsTimeLimited)
            .IsRequired();

        builder.HasMany(ques => ques.Questions)
            .WithOne()
            .HasForeignKey(q => q.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(q => q.QuizCourses)
               .WithOne(qc => qc.Quiz)
               .HasForeignKey(qc => qc.QuizId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

