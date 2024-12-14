using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Configurations;

public class GiftCardConfiguration : IEntityTypeConfiguration<GiftCard>
{
    public void Configure(EntityTypeBuilder<GiftCard> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Code).HasMaxLength(Constants.GiftCardCodeMaxLength).IsRequired();

        builder
            .Property(g => g.Amount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.HasIndex(g => g.Code).IsUnique();

        builder.ToTable("GiftCards", Constants.SchemaName);
    }
}
