

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.ForumAggregate.PostAggregate;

namespace OLMS.Infrastructure.Database.Configurations.ForumModels;

public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne<Post>()
            .WithMany(p => p.Votes)
            .HasForeignKey(v => v.PostId);

        builder.HasOne<UserBase>()
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}

