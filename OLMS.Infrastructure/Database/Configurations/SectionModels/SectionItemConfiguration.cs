using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations.SectionModels;

public sealed class SectionItemConfigurations : IEntityTypeConfiguration<SectionItem>
{
    public void Configure(EntityTypeBuilder<SectionItem> builder)
    {

        builder.HasKey(si => si.Id);

        builder.Property(si => si.Order)
            .IsRequired();

        builder.Property(si => si.ItemType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(si => si.ItemId);

        builder.HasOne<Section>()
            .WithMany(s => s.SectionItems)
            .HasForeignKey(si => si.SectionId);
    }
}


