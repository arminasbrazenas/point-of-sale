using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.PaymentManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.DataAccess.PaymentManagement.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Amount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder
            .Property(p => p.Method)
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .HasConversion(new EnumToStringConverter<PaymentMethod>())
            .IsRequired();

        builder
            .Property(p => p.Status)
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .HasConversion(new EnumToStringConverter<PaymentStatus>())
            .IsRequired();

        builder.HasOne(p => p.Order).WithMany().HasForeignKey(p => p.OrderId).IsRequired();

        builder.HasDiscriminator(p => p.Method).HasValue<CashPayment>(PaymentMethod.Cash);

        builder.ToTable("OrderPayments", Constants.SchemaName);
    }
}
