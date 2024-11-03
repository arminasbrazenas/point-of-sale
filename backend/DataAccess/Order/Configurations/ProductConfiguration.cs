using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Order.Constants;
using PointOfSale.DataAccess.Order.Entities;

namespace PointOfSale.DataAccess.Order.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(ProductConstants.NameMaxLength).IsRequired();

        builder.Property(p => p.Stock).IsRequired();

        builder.Property(p => p.Price).HasPrecision(10, 2).IsRequired();

        builder.HasMany(p => p.Taxes).WithMany(t => t.Products);

        builder.ToTable("Products", OrderConstants.SchemaName);
    }
}
