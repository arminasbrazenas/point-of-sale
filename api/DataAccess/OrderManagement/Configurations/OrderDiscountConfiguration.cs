using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class OrderDiscountConfiguration : IEntityTypeConfiguration<OrderDiscount>
{
    public void Configure(EntityTypeBuilder<OrderDiscount> builder)
    {
        builder.HasKey(d => d.Id);

        builder
            .Property(d => d.Amount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder
            .Property(d => d.AppliedAmount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder
            .Property(d => d.PricingStrategy)
            .HasConversion(new EnumToStringConverter<PricingStrategy>())
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .IsRequired();

        builder
            .Property(d => d.Type)
            .HasConversion(new EnumToStringConverter<OrderDiscountType>())
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .IsRequired();

        builder.ToTable("OrderDiscounts", Constants.SchemaName);
    }
}
