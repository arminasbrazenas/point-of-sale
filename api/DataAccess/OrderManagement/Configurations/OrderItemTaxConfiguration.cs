using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class OrderItemTaxConfiguration : EntityBaseConfiguration<OrderItemTax, int>
{
    private const string TableName = "OrderItemTaxes";

    public override void Configure(EntityTypeBuilder<OrderItemTax> builder)
    {
        base.Configure(builder);

        builder.Property(t => t.Name).HasMaxLength(Constants.TaxNameMaxLength).IsRequired();

        builder
            .Property(m => m.AppliedAmount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
