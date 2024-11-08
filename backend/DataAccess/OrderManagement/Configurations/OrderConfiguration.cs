using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder
            .Property(o => o.Status)
            .HasConversion(new EnumToStringConverter<OrderStatus>())
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .IsRequired();

        builder.HasMany(o => o.Items).WithOne(i => i.Order).HasForeignKey(i => i.OrderId).IsRequired();

        builder.ToTable(Constants.OrderTableName, Constants.SchemaName);
    }
}
