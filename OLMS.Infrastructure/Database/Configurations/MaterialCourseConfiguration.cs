using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities;

namespace OLMS.Infrastructure.Database.Configurations;

internal class MaterialCourseConfiguration : IEntityTypeConfiguration<MaterialCourse>
{
    public void Configure(EntityTypeBuilder<MaterialCourse> builder)
    {
        builder.ToTable("MaterialCourse");

        builder.HasKey(mc => new { mc.MaterialId, mc.CourseId });

        builder.HasOne(mc => mc.Material)
               .WithOne()
               .HasForeignKey<MaterialCourse>(mc => mc.MaterialId)
               .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(mc => mc.Course)
               .WithMany(c => c.MaterialCourse)
               .HasForeignKey(mc => mc.CourseId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
