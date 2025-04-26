using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.SectionEntity;

namespace OLMS.Infrastructure.Database.Configurations.AssignmentModels;

public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.StartDate)
            .IsRequired();

        builder.Property(a => a.DueDate)
            .IsRequired();

        builder.Property(a => a.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(a => a.AllowLateSubmission)
            .IsRequired();

        builder.Property(a => a.NumberOfAttempts)
            .IsRequired();

        builder.HasOne<Instructor>()
            .WithMany()
            .HasForeignKey(a => a.InstructorID);

        builder.HasOne<Section>()
            .WithMany()
            .HasForeignKey(a => a.SectionId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}

