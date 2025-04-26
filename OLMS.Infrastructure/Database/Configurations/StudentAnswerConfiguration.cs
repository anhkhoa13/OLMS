//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OLMS.Domain.Entities.QuizEntity;

//namespace OLMS.Infrastructure.Database.Configurations;

//public sealed class StudentAnswerConfiguration : IEntityTypeConfiguration<StudentResponse>
//{
//    public void Configure(EntityTypeBuilder<StudentResponse> builder)
//    {
//        // Table mapping
//        builder.ToTable("StudentResponse");
//        // Set primary key
//        builder.HasKey(sa => sa.Id);

//        // Configure relationships
//        builder.HasOne(sa => sa.QuizAttempt)
//            .WithMany(q => q.Answers) // Assuming QuizAttempt has a collection of StudentAnswers
//            .HasForeignKey(sa => sa.QuizAttemptId)
//            .OnDelete(DeleteBehavior.Cascade); // Delete answers when attempt is deleted

//        builder.HasOne(sa => sa.Question)
//            .WithMany() // Assuming Question does not track answers
//            .HasForeignKey(sa => sa.QuestionId)
//            .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if Question exists

//        // Set column constraints
//        builder.Property(sa => sa.ResponseText)
//            .IsRequired()
//            .HasMaxLength(500); // Adjust max length as needed
//    }
//}

