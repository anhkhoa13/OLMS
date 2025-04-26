using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.ForumAggregate;

namespace OLMS.Infrastructure.Database.Configurations.ForumModels;

public class ForumConfiguration : IEntityTypeConfiguration<Forum>
{
    public void Configure(EntityTypeBuilder<Forum> builder)
    {
       builder.HasKey(f => f.Id);

        builder.Property(f => f.Title)
                .IsRequired()
                .HasMaxLength(100);

        builder.HasOne<Course>()
            .WithOne(c => c.Forum)
            .HasForeignKey<Forum>(f => f.CourseId);

        builder.HasMany(f => f.Posts)
                .WithOne()
                .HasForeignKey(p => p.ForumId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
