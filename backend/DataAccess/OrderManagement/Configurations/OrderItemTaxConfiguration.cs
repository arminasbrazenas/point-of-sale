using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class OrderItemTaxConfiguration : IEntityTypeConfiguration<OrderItemTax>
{
    private const string TableName = "OrderItemTaxes";

    public void Configure(EntityTypeBuilder<OrderItemTax> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name).HasMaxLength(Constants.TaxNameMaxLength).IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
