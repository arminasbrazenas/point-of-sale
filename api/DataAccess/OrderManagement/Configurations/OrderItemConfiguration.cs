using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    private const string TableName = "OrderItems";

    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name).HasMaxLength(Constants.ProductNameMaxLength).IsRequired();

        builder
            .Property(i => i.BaseUnitPrice)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.HasOne(i => i.Product).WithMany().HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(i => i.Modifiers)
            .WithOne()
            .HasForeignKey(i => i.OrderItemId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(i => i.Taxes)
            .WithOne(t => t.OrderItem)
            .HasForeignKey(t => t.OrderItemId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
