using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class OrderItemDiscountConfiguration : EntityBaseConfiguration<OrderItemDiscount, int>
{
    public override void Configure(EntityTypeBuilder<OrderItemDiscount> builder)
    {
        base.Configure(builder);

        builder.Property(m => m.PricingStrategy).HasMaxLength(SharedConstants.EnumMaxLength).IsRequired();

        builder
            .Property(m => m.AppliedAmount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder
            .Property(d => d.Type)
            .HasConversion(new EnumToStringConverter<OrderDiscountType>())
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .IsRequired();

        builder.ToTable("OrderItemDiscounts", Constants.SchemaName);
    }
}
