using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.PaymentManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.DataAccess.PaymentManagement.Configurations;

public class RefundConfiguration : IEntityTypeConfiguration<PaymentRefund>
{
    public void Configure(EntityTypeBuilder<PaymentRefund> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .Property(r => r.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder
            .Property(r => r.Method)
            .HasMaxLength(50)
            .HasConversion<string>()
            .IsRequired();

        builder
            .Property(r => r.Status)
            .HasMaxLength(50)
            .HasConversion<string>()
            .IsRequired();

        builder
            .Property(r => r.ChargeId)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(r => r.PaymentIntentId)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(r => r.Reason)
            .HasMaxLength(255)
            .IsRequired();

        builder.ToTable("Refunds");
    }
}
