using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities;

namespace OLMS.Infrastructure.Database.Configurations;
public sealed class UserBaseConfigurations : IEntityTypeConfiguration<UserBase>
{
    public void Configure(EntityTypeBuilder<UserBase> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Password).IsRequired();
        builder.Property(u => u.Role)
               .HasConversion<string>()
               .IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
