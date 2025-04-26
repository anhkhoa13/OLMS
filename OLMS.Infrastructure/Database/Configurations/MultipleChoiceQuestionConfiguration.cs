//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OLMS.Domain.Entities.QuestionEntity;
//using OLMS.Domain.Primitives;

//namespace OLMS.Infrastructure.Database.Configurations;

//public sealed class MultipleChoiceQuestionConfiguration : IEntityTypeConfiguration<MultipleChoiceQuestion>
//{
//    public void Configure(EntityTypeBuilder<MultipleChoiceQuestion> builder)
//    {
//        builder.ToTable("MultipleChoiceQuestion");

//        builder.Property(mcq => mcq.CorrectOptionIndex)
//            .IsRequired();

//        builder.Property(mcq => mcq.Options)
//            .HasConversion(
//                v => string.Join(";", v),
//                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
//            );

//        builder.HasOne<Question>()
//            .WithOne()
//            .HasForeignKey<MultipleChoiceQuestion>(mcq => mcq.Id)
//            .OnDelete(DeleteBehavior.Cascade);

//    }
//}

