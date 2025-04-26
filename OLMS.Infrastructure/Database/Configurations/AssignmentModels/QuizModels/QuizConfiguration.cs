using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Primitives;

namespace OLMS.Infrastructure.Database.Configurations.AssignmentModels.QuizModels;

public sealed class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {

        builder.OwnsOne(c => c.Code, c =>
        {
            c.Property(c => c.Value)
             .HasColumnName("Code")
             .IsRequired()
             .HasMaxLength(6);

            c.HasIndex(c => c.Value).IsUnique();
        });

        builder.Property(q => q.IsTimeLimited)
               .IsRequired();

        builder.Property(q => q.TimeLimit)
                .IsRequired(false)
                .HasColumnType("time");

        builder.HasMany(q => q.Questions)
            .WithOne()
            .HasForeignKey(q => q.QuizId);

        builder.HasMany(q => q.QuizCourses)
               .WithOne(qc => qc.Quiz)
               .HasForeignKey(qc => qc.QuizId);
    }
}

