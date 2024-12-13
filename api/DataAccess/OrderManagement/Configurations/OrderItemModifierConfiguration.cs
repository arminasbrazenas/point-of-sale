using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class OrderItemModifierConfiguration : EntityBaseConfiguration<OrderItemModifier, int>
{
    private const string TableName = "OrderItemModifiers";

    public override void Configure(EntityTypeBuilder<OrderItemModifier> builder)
    {
        base.Configure(builder);

        builder.Property(m => m.Name).HasMaxLength(Constants.ModifierNameMaxLength).IsRequired();

        builder
            .Property(m => m.Price)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
