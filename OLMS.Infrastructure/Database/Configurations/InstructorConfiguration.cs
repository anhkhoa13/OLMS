using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructor");

        builder.HasKey(i => i.Id); // Khóa chính dùng chung từ UserBase

        builder.Property(i => i.Id).HasColumnName("Id_instructor");

        builder.Property(i => i.HireDate)
               .HasColumnName("HireDate");

        builder.Property(i => i.Department)
               .HasColumnName("Department")
               .HasMaxLength(100);

        builder.HasOne<UserBase>() 
               .WithOne()
               .HasForeignKey<Instructor>(i => i.Id) 
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.Courses) // Một Instructor có nhiều Course
               .WithOne(c => c.Instructor)
               .HasForeignKey(c => c.InstructorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}


