using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class OrderItemDiscountConfiguration : EntityBaseConfiguration<OrderItemDiscount, int>
{
    public override void Configure(EntityTypeBuilder<OrderItemDiscount> builder)
    {
        base.Configure(builder);

        builder.Property(m => m.PricingStrategy).HasMaxLength(SharedConstants.EnumMaxLength).IsRequired();

        builder
            .Property(m => m.AppliedUnitAmount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.ToTable("OrderItemDiscounts", Constants.SchemaName);
    }
}
