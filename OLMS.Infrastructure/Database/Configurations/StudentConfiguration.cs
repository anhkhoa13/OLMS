using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Student");

        builder.HasKey(i => i.Id); // Khóa chính dùng chung từ UserBase

        builder.HasOne<UserBase>()
               .WithOne()
               .HasForeignKey<Student>(s => s.Id)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
