using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace persistence.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(a => a.productBrand)
                .WithMany()
                .HasForeignKey(a => a.BrandId);


            builder.HasOne(a => a.productType)
                .WithMany()
                .HasForeignKey(a => a.TypeId);


            builder.Property(a => a.Price)
                .HasColumnType("decimal(10,2)");
        }
    }
}
