using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class TaxConfiguration : IEntityTypeConfiguration<Tax>
{
    private const string TableName = "Taxes";

    public void Configure(EntityTypeBuilder<Tax> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name).HasMaxLength(Constants.TaxNameMaxLength).IsRequired();

        builder.Property(t => t.Rate).IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
