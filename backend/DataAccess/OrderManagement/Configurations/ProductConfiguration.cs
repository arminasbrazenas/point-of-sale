using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(Constants.ProductNameMaxLength).IsRequired();

        builder.Property(p => p.Stock).IsRequired();

        builder.Property(p => p.Price).HasPrecision(10, 2).IsRequired();

        builder.HasMany(p => p.Taxes).WithMany(t => t.Products);

        builder.ToTable(Constants.ProductTableName, Constants.SchemaName);
    }
}
