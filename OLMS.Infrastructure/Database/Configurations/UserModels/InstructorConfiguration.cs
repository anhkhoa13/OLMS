using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.InstructorAggregate;


namespace OLMS.Infrastructure.Database.Configurations.UserModels;

public sealed class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructor");

        builder.Property(i => i.Department)
               .HasColumnName("Department")
               .HasMaxLength(100);

        builder.HasOne<UserBase>()
               .WithOne()
               .HasForeignKey<Instructor>(i => i.Id)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.Courses)
            .WithOne()
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


