using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities;

namespace OLMS.Infrastructure.Database.Configurations.UserModels;
public sealed class UserBaseConfigurations : IEntityTypeConfiguration<UserBase>
{
    public void Configure(EntityTypeBuilder<UserBase> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.Id);
        //builder.Property(u => u.Id).HasColumnName("ID_user");

        builder.OwnsOne(u => u.Username, username =>
        {
            username.Property(username => username.Value)
             .HasColumnName("Username")
             .IsRequired()
             .HasMaxLength(50);

            username.HasIndex(username => username.Value).IsUnique();
        });
        builder.OwnsOne(u => u.Password, password =>
        {
            password.Property(password => password.Value)
             .HasColumnName("Password")
             .HasMaxLength(32)
             .IsRequired();
        });
        builder.OwnsOne(u => u.FullName, fullname =>
        {
            fullname.Property(fullname => fullname.Value)
             .HasColumnName("FullName")
             .IsRequired()
             .HasMaxLength(100);
        });
        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(email => email.Value)
             .HasColumnName("Email")
             .IsRequired()
             .HasMaxLength(100);

            email.HasIndex(email => email.Value).IsUnique();
        });
        builder.Property(u => u.Role)
               .HasConversion<string>()
               .HasMaxLength(20)
               .IsRequired();
    }
}
