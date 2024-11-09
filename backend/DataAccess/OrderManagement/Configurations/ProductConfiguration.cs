using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private const string TableName = "Products";

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(Constants.ProductNameMaxLength).IsRequired();

        builder.Property(p => p.Stock).IsRequired();

        builder
            .Property(p => p.Price)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.HasMany(p => p.Taxes).WithMany(t => t.Products);

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
