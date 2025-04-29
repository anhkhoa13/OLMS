using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;

public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement> {
    public void Configure(EntityTypeBuilder<Announcement> builder) {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Content)
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        // Configure relationship with Course
        builder.HasOne<Course>()
            .WithMany(c => c.Announcements)
            .HasForeignKey(a => a.CourseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
