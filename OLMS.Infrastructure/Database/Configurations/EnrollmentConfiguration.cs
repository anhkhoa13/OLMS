//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OLMS.Domain.Entities;

//namespace OLMS.Infrastructure.Database.Configurations;

//public sealed class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
//{
//    public void Configure(EntityTypeBuilder<Enrollment> builder)
//    {
//        builder.ToTable("Enrollment");

//        builder.HasKey(e => new { e.StudentId, e.ForumId });

//        builder.HasOne(e => e.Student)
//               .WithMany()
//               .HasForeignKey(e => e.StudentId)
//               .OnDelete(DeleteBehavior.Cascade);

//        builder.HasOne(e => e.Course)
//               .WithMany(c => c.Enrollments)
//               .HasForeignKey(e => e.ForumId)
//               .OnDelete(DeleteBehavior.Cascade);
//    }
//}
