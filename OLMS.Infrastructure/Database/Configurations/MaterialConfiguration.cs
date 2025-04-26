//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OLMS.Domain.Entities;

//namespace OLMS.Infrastructure.Database.Configurations;

//public class MaterialConfiguration : IEntityTypeConfiguration<Material>
//{
//    public void Configure(EntityTypeBuilder<Material> builder)
//    {
//        builder.ToTable("Material");

//        builder.Property(m => m.Id)
//                .HasColumnName("MaterialId");

//        builder.HasKey(m => m.Id);

//        builder.Property(m => m.FileName)
//                .IsRequired()
//                .HasMaxLength(255);

//        builder.Property(m => m.ContentType)
//                .IsRequired()
//                .HasMaxLength(100);

//        builder.Property(m => m.MaterialType)
//                .IsRequired();

//        builder.Property(m => m.FileSize)
//                .IsRequired();

//        builder.Property(m => m.Data)
//                .IsRequired();

//        builder.Property(m => m.UploadDate)
//                .IsRequired()
//                .HasDefaultValueSql("GETDATE()");

//        builder.HasOne(m => m.User)
//            .WithMany()
//            .HasForeignKey(m => m.UserId)
//            .OnDelete(DeleteBehavior.SetNull);
//    }
//}
