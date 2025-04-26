//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OLMS.Domain.Entities.QuizEntity;
//using OLMS.Domain.Primitives;

//namespace OLMS.Infrastructure.Database.Configurations;

//public sealed class QuizCourseConfiguration : IEntityTypeConfiguration<QuizCourse>
//{
//    public void Configure(EntityTypeBuilder<QuizCourse> builder)
//    {
//        builder.HasKey(qc => new { qc.QuizId, qc.ForumId });

//        builder.HasOne(qc => qc.Quiz)
//               .WithMany(q => q.QuizCourses)
//               .HasForeignKey(qc => qc.QuizId);

//        builder.HasOne(qc => qc.Course)
//               .WithMany(c => c.QuizCourses)
//               .HasForeignKey(qc => qc.ForumId);
//    }
//}

