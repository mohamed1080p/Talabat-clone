
using Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace persistence.Data.Configurations
{
    internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(a => a.Price).HasColumnType("decimal(8,2)");
            builder.Property(a => a.ShortName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(a => a.Description).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(a => a.DeliveryTime).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
