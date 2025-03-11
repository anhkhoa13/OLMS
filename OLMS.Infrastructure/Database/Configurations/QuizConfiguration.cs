using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Primitives;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.ToTable("Quiz");

        builder.HasKey(q => q.Id);

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
    }
}

