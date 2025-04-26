//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OLMS.Domain.Entities.QuestionEntity;
//using OLMS.Domain.Primitives;

//namespace OLMS.Infrastructure.Database.Configurations;

//public sealed class ShortAnswerQuestionConfiguration : IEntityTypeConfiguration<ShortAnswerQuestion>
//{
//    public void Configure(EntityTypeBuilder<ShortAnswerQuestion> builder)
//    {
//        builder.ToTable("ShortAnswerQuestion");

//        /*// Configure primary key (inherited from Question)
//        builder.HasKey(q => q.Id);*/

//        // Ensure CorrectAnswer is required and has a reasonable max length
//        builder.Property(q => q.CorrectAnswer)
//            .IsRequired()
//            .HasMaxLength(500); // Adjust as needed

//        builder.HasOne<Question>()
//            .WithOne()
//            .HasForeignKey<ShortAnswerQuestion>(saq => saq.Id)
//            .OnDelete(DeleteBehavior.Cascade);

//    }
//}

