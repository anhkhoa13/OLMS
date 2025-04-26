using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations;

public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment> {
    public void Configure(EntityTypeBuilder<Assignment> builder) {
        builder.ToTable("Assignments");

        // Primary key
        builder.HasKey(a => a.Id);

        // Properties
        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Description)
            .IsRequired();

        builder.Property(a => a.StartDate)
            .IsRequired();

        builder.Property(a => a.DueDate)
            .IsRequired();

        builder.Property(a => a.Type)
            .IsRequired();

        builder.Property(a => a.AllowLateSubmission)
            .IsRequired();

        builder.Property(a => a.NumberOfAttempts)
            .IsRequired();

        builder.Property(a => a.SectionId)
            .IsRequired();

        builder.Property(a => a.InstructorID)
            .IsRequired();

        // Configure the privately owned collection _assignmentAttempts
        builder.Metadata.FindNavigation(nameof(Assignment.AssignmentAttempts))
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        // TPH inheritance strategy
        //builder.HasDiscriminator(a => a.Type);
    }
}

