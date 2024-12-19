using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Configurations;

public class CardPaymentConfiguration : EntityBaseConfiguration<CardPayment, int>
{
    public override void Configure(EntityTypeBuilder<CardPayment> builder)
    {
        builder.Property(p => p.ExternalId).HasMaxLength(100).IsRequired();
    }
}
