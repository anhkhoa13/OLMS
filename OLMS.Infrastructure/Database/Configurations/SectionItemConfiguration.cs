using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Infrastructure.Database.Configurations;

public sealed class SectionItemConfigurations : IEntityTypeConfiguration<SectionItem>
{
    public void Configure(EntityTypeBuilder<SectionItem> builder)
    {
        builder.ToTable("SectionItem");

        builder.HasKey(si => si.Id);
        builder.Property(si => si.Order).IsRequired();
        builder.Property(si => si.ItemId).IsRequired();
        builder.Property(si => si.SectionId).IsRequired();
    }
}


