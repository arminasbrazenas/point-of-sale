using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class ProductConfiguration : EntityBaseConfiguration<Product, int>
{
    private const string TableName = "Products";
    private const string ProductTaxesTableName = "ProductTaxes";
    private const string ProductModifiersTableName = "ProductModifiers";

    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.RowVersion).IsRowVersion();

        builder.Property(p => p.Name).HasMaxLength(Constants.ProductNameMaxLength).IsRequired();

        builder.Property(p => p.Stock).IsRequired();

        builder
            .Property(p => p.Price)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.HasMany(p => p.Taxes).WithMany(t => t.Products).UsingEntity(e => e.ToTable(ProductTaxesTableName));

        builder
            .HasMany(p => p.Modifiers)
            .WithMany(v => v.Products)
            .UsingEntity(e => e.ToTable(ProductModifiersTableName));

        builder.HasOne(o => o.Business).WithMany().HasForeignKey(o => o.BusinessId).IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
