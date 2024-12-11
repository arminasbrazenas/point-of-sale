using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class OrderServiceChargeConfiguration : EntityBaseConfiguration<OrderServiceCharge, int>
{
    private const string TableName = "OrderServiceCharges";

    public override void Configure(EntityTypeBuilder<OrderServiceCharge> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name).HasMaxLength(Constants.ServiceChargeNameMaxLength).IsRequired();

        builder
            .Property(c => c.PricingStrategy)
            .HasConversion(new EnumToStringConverter<PricingStrategy>())
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .IsRequired();

        builder
            .Property(c => c.Amount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder
            .Property(m => m.AppliedAmount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
