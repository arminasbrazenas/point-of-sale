using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class ModifierConfiguration : IEntityTypeConfiguration<Modifier>
{
    private const string TableName = "Modifiers";
    
    public void Configure(EntityTypeBuilder<Modifier> builder)
    {
        builder.HasKey(v => v.Id);
        
        builder.Property(p => p.RowVersion).IsRowVersion();

        builder.Property(v => v.Name).HasMaxLength(Constants.ModifierNameMaxLength).IsRequired();

        builder.Property(v => v.Price).HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale).IsRequired();

        builder.ToTable(TableName, Constants.SchemaName);
    }
}