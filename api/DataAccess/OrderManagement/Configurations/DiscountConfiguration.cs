using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.Shared.Enums;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class DiscountConfiguration : EntityBaseConfiguration<Discount, int>
{
    private const string TableName = "Discounts";
    private const string ProductDiscountsTableName = "ProductDiscounts";

    public override void Configure(EntityTypeBuilder<Discount> builder)
    {
        base.Configure(builder);

        builder
            .Property(d => d.Amount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder
            .Property(d => d.PricingStrategy)
            .HasConversion(new EnumToStringConverter<PricingStrategy>())
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .IsRequired();

        builder
            .HasMany(d => d.AppliesTo)
            .WithMany(d => d.Discounts)
            .UsingEntity(e => e.ToTable(ProductDiscountsTableName));

        builder.HasOne(o => o.Business).WithMany().HasForeignKey(o => o.BusinessId).IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
