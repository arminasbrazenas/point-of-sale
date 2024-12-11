using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class ModifierConfiguration : EntityBaseConfiguration<Modifier, int>
{
    private const string TableName = "Modifiers";

    public override void Configure(EntityTypeBuilder<Modifier> builder)
    {
        base.Configure(builder);
        
        builder.Property(p => p.RowVersion).IsRowVersion();

        builder.Property(v => v.Name).HasMaxLength(Constants.ModifierNameMaxLength).IsRequired();

        builder
            .Property(v => v.Price)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
