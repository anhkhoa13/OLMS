using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.ForumAggregate;
using OLMS.Domain.Entities.ForumAggregate.PostAggregate;


namespace OLMS.Infrastructure.Database.Configurations.ForumModels;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Body)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.VoteScore);

        builder.HasMany(p => p.Votes)
            .WithOne()
            .HasForeignKey(v => v.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Comments)
            .WithOne()
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Forum>()
            .WithMany(f => f.Posts)
            .HasForeignKey(p => p.ForumId);

    }
}

