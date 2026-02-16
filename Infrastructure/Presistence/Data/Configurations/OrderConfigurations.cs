using Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace persistence.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(a => a.SubTotal).HasColumnType("decimal(8,2)");
            builder.HasMany(a => a.Items).WithOne();
            builder.HasOne(a => a.DeliveryMethod).WithMany().HasForeignKey(a => a.DeliveryMethodId);
            builder.OwnsOne(a => a.Address);
        }
    }
}
