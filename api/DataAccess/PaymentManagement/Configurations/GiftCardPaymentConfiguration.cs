using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Configurations;

public class GiftCardPaymentConfiguration : EntityBaseConfiguration<GiftCardPayment, int>
{
    public override void Configure(EntityTypeBuilder<GiftCardPayment> builder)
    {
        builder.Property(g => g.GiftCardCode).HasMaxLength(Constants.GiftCardCodeMaxLength);
    }
}
