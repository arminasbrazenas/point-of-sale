using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class TaxConfiguration : EntityBaseConfiguration<Tax, int>
{
    private const string TableName = "Taxes";

    public override void Configure(EntityTypeBuilder<Tax> builder)
    {
        base.Configure(builder);

        builder.Property(t => t.Name).HasMaxLength(Constants.TaxNameMaxLength).IsRequired();

        builder.Property(t => t.Rate).IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
