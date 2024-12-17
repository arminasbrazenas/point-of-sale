using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Configurations;

public class OnlinePaymentConfiguration : EntityBaseConfiguration<OnlinePayment, int>
{
    public override void Configure(EntityTypeBuilder<OnlinePayment> builder)
    {
        builder.Property(p => p.ExternalId).HasMaxLength(100).IsRequired();
    }
}
