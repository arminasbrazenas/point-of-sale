using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Order.Constants;
using PointOfSale.DataAccess.Order.Entities;

namespace PointOfSale.DataAccess.Order.Configurations;

public class TaxConfiguration : IEntityTypeConfiguration<Tax>
{
    public void Configure(EntityTypeBuilder<Tax> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name).HasMaxLength(TaxConstants.NameMaxLength).IsRequired();

        builder.Property(t => t.Rate).IsRequired();

        builder.ToTable("Taxes", OrderConstants.SchemaName);
    }
}
